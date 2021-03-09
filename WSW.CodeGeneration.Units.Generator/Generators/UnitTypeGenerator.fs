module UnitTypeGenerator

open WSW.CodeGeneration.Units.Generator

let CreateConstructor = 
    let valueParam = "value" |> Parameters.CreateParameter Types.DoubleType
    Members.PublicConstructor [valueParam.Declaration] [valueParam.ValueExpression] []

let YieldUnitName (unit:UnitDefinition) = 
    (sprintf "yield return nameof(%s)" unit.Name) |> Expressions.Snippet |> Statements.MakeStatement

let CreateUnitList unitType =
    let yieldStatements = unitType.Units |> List.map YieldUnitName
    Members.CreateStaticProperty "UnitNames" Types.IEnumerableOfStringsType yieldStatements

let TestForUnitThenReturn valueToTest (unit:UnitDefinition) thingToReturn =
    let testUnit = Expressions.CompareValues valueToTest (unit.Name |> Expressions.PrimitiveValue)
    Statements.IfStatement testUnit [thingToReturn |> Statements.MethodReturn]

let HandleConvertToUnit unitParam (unit:UnitDefinition) =
    let createFromSi = Expressions.CallStaticMethod unit.Name "FromSI" ["SIValue" |> Expressions.PropertyReference]
    TestForUnitThenReturn unitParam unit createFromSi

let CreateTryConvert (unitType:UnitTypeDefinition) =
    let unitParam = Parameters.CreateParameter Types.StringType "unit"
    let convertStatements = unitType.Units |> List.map (HandleConvertToUnit unitParam.ValueExpression)
    let defaultNull = Expressions.NullValueExpression |> Statements.MethodReturn
    let allStatements = convertStatements @ [defaultNull]
    Members.CreatePublicOverrideFunction "TryConvert" "ValueWithUnit" [unitParam.Declaration] allStatements

let HandleCreateUnit valueParam unitParam (unit:UnitDefinition) =
    let create = Expressions.CallStaticMethod unit.Name (sprintf "From%s" unit.Name) [valueParam]
    TestForUnitThenReturn unitParam unit create

let CreateTryCreate (unitType:UnitTypeDefinition) =
    let valueParam = Parameters.CreateParameter Types.DoubleType "value"
    let unitParam = Parameters.CreateParameter Types.StringType "unit"
    let createStatements = unitType.Units |> List.map (HandleCreateUnit valueParam.ValueExpression unitParam.ValueExpression)
    let defaultNull = Expressions.NullValueExpression |> Statements.MethodReturn
    let allStatements = createStatements @ [defaultNull]
    Members.CreateStaticFunction "TryCreate" (unitType |> TypeNames.UnitTypeClassName) [valueParam.Declaration; unitParam.Declaration] allStatements

let GenerateUnitType unitType =
    let constructor = CreateConstructor
    let unitNamesList = unitType |> CreateUnitList
    let tryConvert = unitType |> CreateTryConvert
    let tryCreate = unitType |> CreateTryCreate

    let resultClass = Classes.CreateAbstractClass (unitType |> TypeNames.UnitTypeClassName) ("ValueWithUnit" |> Some)
                                                   [
                                                        constructor
                                                        unitNamesList
                                                        tryConvert
                                                        tryCreate
                                                   ]
    resultClass |> Namespaces.WrapInNamespaceAndUnit