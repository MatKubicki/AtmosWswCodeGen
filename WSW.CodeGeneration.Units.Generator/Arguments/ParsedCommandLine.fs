namespace WSW.CodeGeneration.Units.Generator

type ParsedCommandLine =
    | NeedHelp
    | Default
    | Valid of ValidArguments

module ParsedCommandLine =

    let ParseArguments (arguments:string list) =
        match arguments with
        | ["-?"] -> NeedHelp
        | [] -> Default
        | [x] -> x |> ValidArguments.Create |> Valid
        | _ -> NeedHelp