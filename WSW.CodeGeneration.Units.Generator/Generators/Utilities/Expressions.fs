module Expressions

open System.CodeDom
open System

let AsExpression (expression:'T when 'T :> CodeExpression) = expression :> CodeExpression

let PropertyReference (propertyName) = new CodePropertyReferenceExpression(null, propertyName) |> AsExpression

let StaticPropertyReference (typ:string) (propertyName) = 
    let typeReference = new CodeTypeReference(typ)
    new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeReference), propertyName) |> AsExpression

let CallMethod (name) (arguments:CodeExpression List) =
    new CodeMethodInvokeExpression(null, name, Array.ofList(arguments)) |> AsExpression
        
let CallStaticMethod (typ:string) (name) (arguments:CodeExpression list) =
    let typeReference = new CodeTypeReferenceExpression(typ)
    new CodeMethodInvokeExpression(typeReference, name, arguments |> Array.ofList) |> AsExpression

let CreateObject (typ:string) (arguments:CodeExpression list) = 
    new CodeObjectCreateExpression(new CodeTypeReference(typ), Array.ofList(arguments)) |> AsExpression


let Snippet (code:string) = new CodeSnippetExpression(code) |> AsExpression
let NullValueExpression = new CodePrimitiveExpression(null) |> AsExpression
let PrimitiveValue (value) = new CodePrimitiveExpression(value) |> AsExpression
let LiteralString (value:string) = new CodePrimitiveExpression(value) |> AsExpression

let CompareValues (left) (right) =  new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right) |> AsExpression