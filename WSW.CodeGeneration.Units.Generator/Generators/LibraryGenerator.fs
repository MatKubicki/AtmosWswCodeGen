module LibraryGenerator
open WSW.CodeGeneration.Units.Generator

let YieldUnitTypeName (unitType:UnitTypeDefinition) = 
    (sprintf "yield return \"%s\"" unitType.Name) |> Expressions.Snippet |> Statements.MakeStatement

let CreateUnitTypeList (unitTypes:UnitTypeDefinition list) =
    let yieldStatements = unitTypes |> List.map YieldUnitTypeName
    Members.CreateStaticProperty "UnitTypeNames" Types.IEnumerableOfStringsType yieldStatements

let TestForUnitTypeThenReturn valueToTest (unitType:UnitTypeDefinition) thingToReturn =
    let testUnit = Expressions.CompareValues valueToTest (unitType.Name |> Expressions.PrimitiveValue)
    Statements.IfStatement testUnit [thingToReturn |> Statements.MethodReturn]

let HandleRequestForUnitType unitTypeParam (unitType:UnitTypeDefinition) =
    let getUnits = Expressions.StaticPropertyReference (unitType |> TypeNames.UnitTypeClassName) "UnitNames"
    TestForUnitTypeThenReturn unitTypeParam unitType getUnits

let CreateUnitNameList (unitTypes:UnitTypeDefinition list) =
    let unitTypeParam = Parameters.CreateParameter Types.StringType "unitType"
    let listStatements = unitTypes |> List.map (HandleRequestForUnitType unitTypeParam.ValueExpression)

    let defaultNull = Expressions.NullValueExpression |> Statements.MethodReturn
    let allStatements = listStatements @ [defaultNull]
    Members.CreateStaticFunction "TryGetUnits" "IEnumerable<string>" [unitTypeParam.Declaration] allStatements

let HandleTryCreateForUnitType unitTypeParam valueParam unitParam (unitType:UnitTypeDefinition)  =
    let tryCreate = Expressions.CallStaticMethod (unitType |> TypeNames.UnitTypeClassName) "TryCreate" [valueParam; unitParam]
    TestForUnitTypeThenReturn unitTypeParam unitType tryCreate

let CreateTryCreate (unitTypes:UnitTypeDefinition list) =
    let unitTypeParam = Parameters.CreateParameter Types.StringType "unitType"
    let valueParam = Parameters.CreateParameter Types.DoubleType "value"
    let unitParam = Parameters.CreateParameter Types.StringType "unit"

    let listStatements = unitTypes |> List.map (HandleTryCreateForUnitType unitTypeParam.ValueExpression valueParam.ValueExpression unitParam.ValueExpression)
    let defaultNull = Expressions.NullValueExpression |> Statements.MethodReturn
    let allStatements = listStatements @ [defaultNull]
    Members.CreateStaticFunction "TryCreate" "ValueWithUnit" [unitTypeParam.Declaration; valueParam.Declaration; unitParam.Declaration] allStatements

let GenerateLibrary (unitTypes:UnitTypeDefinition list) =
    let unitTypeList = unitTypes |> CreateUnitTypeList
    let unitNameList = unitTypes |> CreateUnitNameList
    let tryCreate = unitTypes |> CreateTryCreate

    let resultClass = Classes.CreatePublicClass "UnitLibrary" None
                                                   [
                                                        unitTypeList
                                                        unitNameList
                                                        tryCreate
                                                   ]
    resultClass |> Namespaces.WrapInNamespaceAndUnit