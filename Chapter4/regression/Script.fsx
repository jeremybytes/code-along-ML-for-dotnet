#I @"C:\Users\jerem\.nuget\packages\"
#r @"fsharp.data\4.2.2\lib\netstandard2.0\FSharp.Data.dll"

open FSharp.Data

type Data = CsvProvider<"day.csv">
let dataset = Data.Load("day.csv")
let data = dataset.Rows

// Abandonding at FSharp.Charting which requires .NET Framework