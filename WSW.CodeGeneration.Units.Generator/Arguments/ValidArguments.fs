namespace WSW.CodeGeneration.Units.Generator

type OutputRoot = OutputRoot of string

type ValidArguments = 
    {
        OutputPath : OutputRoot
    }

module ValidArguments =

    let Create path = {OutputPath = OutputRoot path}