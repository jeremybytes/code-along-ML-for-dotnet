#I @"C:\Users\jerem\.nuget\packages\"
#r @"fsharp.data\4.2.2\lib\netstandard2.0\FSharp.Data.dll"

open FSharp.Data

let wb = WorldBankData.GetDataContext()
wb.Countries.Japan.CapitalCity

let countries = wb.Countries

let pop2010 = [ for c in countries -> c.Indicators.``Population, total``.[2010]]
let pop2020 = [ for c in countries -> c.Indicators.``Population, total``.[2020]]

///////////////////////////////////////////////
///
///  Note: I could not get the R NuGet package
///  to work with .NET 5
///  Most internet searches took me to old
///  stuff, so this needs more research
/// 
///////////////////////////////////////////////

#r @"r.net\1.7.0\lib\net40\RDotNet.dll"
#r @"rprovider\1.1.22\lib\net40\RProvider.Runtime.dll"
#r @"rprovider\1.1.22\lib\net40\RProvider.dll"

open RProvider
open RProvider.``base``
open RProvider.graphics

let surface = [ for c in countries -> c.Indicators.``Surface area (sq. km)``.[2010]]
R.summary(surface) |> R.print
