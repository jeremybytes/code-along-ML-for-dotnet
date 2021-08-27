#I @"C:\Users\jerem\.nuget\packages\"
#r @"fsharp.data\4.2.2\lib\netstandard2.0\FSharp.Data.dll"
#r @"fsharp.data\4.2.2\typeproviders\fsharp41\netstandard2.0\FSharp.Data.DesignTime.dll"

open FSharp.Data

type Questions = JsonProvider<"""https://api.stackexchange.com/2.3/questions?site=stackoverflow""">

let csQuestions = """https://api.stackexchange.com/2.3/questions?page=20&order=desc&sort=activity&tagged=C%23&site=stackoverflow"""

Questions.Load(csQuestions).Items |> Seq.iter (fun q -> printfn "%s" q.Title)

// DSL

let questionQuery = """https://api.stackexchange.com/2.3/questions?site=stackoverflow"""

let tagged tags query =
    // join the tags in a ; separated string
    let joinedTags = tags |> String.concat ";"
    sprintf"%s&tagged=%s" query joinedTags

let page p query = sprintf "%s&page=%i" query p

let pageSize s query = sprintf "%s&pagesize=%i" query s

let extractQuestions (query:string) = Questions.Load(query).Items

let ``C#`` = "C%23"
let ``F#`` = "F%23"

let fsSample =
    questionQuery
    |> tagged [``F#``]
    |> pageSize 100
    |> extractQuestions

let csSample = 
    questionQuery
    |> tagged [``C#``]
    |> pageSize 100
    |> extractQuestions

let analyzeTags (qs:Questions.Item seq) =
    qs
    |> Seq.collect (fun question -> question.Tags)
    |> Seq.countBy id
    |> Seq.filter (fun (_,count) -> count > 2)
    |> Seq.sortBy (fun (_,count) -> -count)
    |> Seq.iter (fun (tag,count) -> printfn "%s,%i" tag count)

analyzeTags fsSample
analyzeTags csSample