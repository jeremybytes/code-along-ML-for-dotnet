open System.IO

type Observation = { Label:string; Pixels:int[] }
type Distance = int[] * int[] -> int

let toObservation (csvData:string) =
    let columns = csvData.Split(",")
    let label = columns.[0]
    let pixels = columns.[1..] |> Array.map int
    { Label = label; Pixels = pixels }

let reader path =
    let data = File.ReadAllLines path
    data.[1..]
    |> Array.map toObservation

let trainingPath = @"..\data\trainingsample.csv"
let trainingData = reader trainingPath

// loaded to here

let manhattanDistance (pixels1,pixels2) =
    Array.zip pixels1 pixels2
    |> Array.sumBy (fun (x,y) -> abs (x-y))

let euclideanDistance (pixels1,pixels2) =
    Array.zip pixels1 pixels2
    |> Array.sumBy (fun (x,y) -> pown (x-y) 2)

let train (trainingset:Observation[]) (dist:Distance) =
    let classify (pixels:int[]) =
        trainingset
        |> Array.minBy(fun x -> dist (x.Pixels, pixels))
        |> fun x -> x.Label
    classify

let classifier = train trainingData

let validationPath = @"..\data\validationsample.csv"
let validationData = reader validationPath

let evaluate validationSet classifier =
    validationSet
    |> Array.averageBy (fun x -> if classifier x.Pixels = x.Label then 1. else 0.)
    |> printfn "Correct: %.3f"

let manhattanClassifier = train trainingData manhattanDistance
let euclideanClassifier = train trainingData euclideanDistance 

printfn "Manhattan"
evaluate validationData manhattanClassifier

printfn "Euclidean"
evaluate validationData euclideanClassifier
