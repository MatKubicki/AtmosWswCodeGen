// Learn more about F# at http://fsharp.org

open System
open WSW.CodeGeneration.Units.Generator

let RunConfig config =
    for file in Worker.Run config do
        printfn "Writing: %s" file
    0

[<EntryPoint>]
let main argv =
    printfn "Unit Generator"
    let arguments = argv |> List.ofArray |> ParsedCommandLine.ParseArguments
    match arguments with
    | Default -> "..\..\..\..\WSW.CodeGeneration.Units.Definitions" |> ValidArguments.Create |> RunConfig
    | Valid config -> config |> RunConfig
    | NeedHelp -> 
          printfn "Provide the path to the unit definitions code"
          -1
