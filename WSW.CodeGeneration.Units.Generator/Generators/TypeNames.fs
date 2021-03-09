module TypeNames

open WSW.CodeGeneration.Units.Generator

let UnitTypeClassName (unitType:UnitTypeDefinition) = unitType.Name + "Value";
let UnitClassName (unit:UnitDefinition) = unit.Name;

