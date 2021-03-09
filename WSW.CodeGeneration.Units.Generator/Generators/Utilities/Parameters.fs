namespace WSW.CodeGeneration.Units.Generator
open System.CodeDom
open System

type ParameterInfo = 
    {
        Declaration : CodeParameterDeclarationExpression
        ValueExpression : CodeExpression
    }

module Parameters =

    let CreateParameter (typ:CodeTypeReference) (name) =
        {Declaration = new CodeParameterDeclarationExpression(typ, name); ValueExpression = new CodeArgumentReferenceExpression(name)}