open System.IO

type DocType =
    | Ham
    | Spam

let parseDocType (label:string) =
    match label with
    | "ham" -> Ham
    | "spam" -> Spam
    | _ -> failwith "Unknown label"

let parseLine (line:string) =
    let split = line.Split('\t')
    let label = split.[0] |> parseDocType
    let message = split.[1]
    (label, message)

let fileName = "SMSSpamCollection"
let path = __SOURCE_DIRECTORY__ + @"\..\data\" + fileName

//printfn "%s" path

let dataset =
    File.ReadAllLines path
    |> Array.map parseLine

let spamWithFree =
    dataset
    |> Array.filter (fun (docType,_) -> docType = Spam)
    |> Array.filter (fun (_,sms) -> sms.Contains("FREE"))
    |> Array.length

let hamWithFree = 
    dataset
    |> Array.filter (fun (docType,_) -> docType = Ham)
    |> Array.filter (fun (_,sms) -> sms.Contains("FREE"))
    |> Array.length

let primitiveClassifier (sms:string) =
    if (sms.Contains "FREE")
    then Spam
    else Ham


open System.Text.RegularExpressions

let matchWords = Regex(@"\w+")

let wordTokenizer (text:string) =
    text.ToLowerInvariant()
    |> matchWords.Matches
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Value)
    |> Set.ofSeq

#load "NaiveBayes.fs"
open NaiveBayes.Classifier

let validation, training = dataset.[..999], dataset.[1000..]

let txtClassifier = train training wordTokenizer (["txt"] |> set)

validation
|> Seq.averageBy (fun (docType,sms) ->
    if docType = txtClassifier sms then 1.0 else 0.0)
|> printfn "Based on 'txt', correctly classified: %.3f"