namespace WSW.CodeGeneration.Units.Generator

type UnitDefinition =
    {
        Name : string
        Scalar : double        
        Suffix : string
    }

type UnitTypeDefinition =
    {
        Name : string
        Units : UnitDefinition list
    }

module Helpers =

    let Unit name scalar suffix = {Name = name; Scalar = scalar; Suffix = suffix}
    let UnitType name units = {Name = name; Units = units}