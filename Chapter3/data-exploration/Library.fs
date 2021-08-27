namespace data_exploration

module Test =

    open FSharp.Data

    type Questions = JsonProvider<"""https://api.stackexchange.com/2.3/questions?site=stackoverflow""">


