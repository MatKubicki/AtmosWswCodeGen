module Statements
open System.CodeDom

let AsStatement (statement:'T when 'T :> CodeStatement) = statement :> CodeStatement
let MakeStatement expression = new CodeExpressionStatement(expression) |> AsStatement

let MethodReturn (expression:CodeExpression) = new CodeMethodReturnStatement(expression) |> AsStatement
        
let IfStatement (test) (trueStatements:CodeStatement list) =
    new CodeConditionStatement(test, trueStatements |> List.toArray) |> AsStatement