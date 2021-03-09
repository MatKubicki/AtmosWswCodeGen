namespace WSW.CodeGeneration.Units.Generator

module Worker =

    let GenerateUnit unitType unit = 
        {FileName=unit |> TypeNames.UnitClassName; FolderName = unitType.Name; Code = unit |> UnitGenerator.GenerateUnit unitType}

    let GenerateUnitType (unitType:UnitTypeDefinition) = 
        [{FileName=unitType |> TypeNames.UnitTypeClassName; FolderName = unitType.Name; Code = unitType |> UnitTypeGenerator.GenerateUnitType}]
        @
        (unitType.Units |> List.map (GenerateUnit unitType))

    let GenerateFiles (unitTypes) = 
        let perUnitFiles = unitTypes |> List.map GenerateUnitType |> List.concat
        let libraryFile = [{FileName="UnitLibrary"; FolderName = "Library"; Code = unitTypes |> LibraryGenerator.GenerateLibrary}]
        perUnitFiles @ libraryFile

    let Run (arguments:ValidArguments) =
        CodeWriter.DeleteAllCode arguments.OutputPath
        let WriteSingleFile f = f |> CodeWriter.WriteCode arguments.OutputPath
        Definitions.GetDefinitions |> GenerateFiles |> List.map WriteSingleFile
