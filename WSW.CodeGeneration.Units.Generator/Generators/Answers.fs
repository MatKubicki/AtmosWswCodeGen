module Hints

//And by hints I mean answers

//let CreateScalarProperty (unit:UnitDefinition) = 
//    Members.CreateProtectedProperty "Scalar" Types.DoubleType [unit.Scalar |> Expressions.PrimitiveValue |> Statements.MethodReturn]

//let CreateFromUnitMethod (unit:UnitDefinition) = 
//    let inputParam = Parameters.CreateParameter Types.DoubleType "value"
//    let methodName = "From" + unit.Name
//    let createResult = Expressions.CreateObject unit.Name [inputParam.ValueExpression]
//    Members.CreateStaticFunction methodName unit.Name [inputParam.Declaration] [createResult |> Statements.MethodReturn]

//let CreateFromSiMethod (unit:UnitDefinition) = 
//    let inputParam = Parameters.CreateParameter Types.DoubleType "siValue"
//    let convertValue = Expressions.CallMethod "ConvertValueFromSI" [inputParam.ValueExpression; unit.Scalar |> Expressions.PrimitiveValue]
//    let createResult = Expressions.CreateObject unit.Name [convertValue]
//    Members.CreateStaticFunction "FromSI" unit.Name [inputParam.Declaration] [createResult |> Statements.MethodReturn]