module Members

open System.CodeDom

let AsMember (codeMember:'T when 'T :> CodeTypeMember) = codeMember :> CodeTypeMember

let CreateProperty (attributes:MemberAttributes)
                   (name:string)
                   (typ:CodeTypeReference)
                   (getStatements:CodeStatement list)
                   =
    let result = new CodeMemberProperty(Name = name, Type=typ, Attributes = attributes)    
    result.GetStatements.AddRange(getStatements |> Array.ofList)
    result |> AsMember

let CreateStaticProperty name typ getStatements= CreateProperty (MemberAttributes.Public ||| MemberAttributes.Static) name typ getStatements
let CreatePublicProperty name typ getStatements= CreateProperty (MemberAttributes.Public ||| MemberAttributes.Override) name typ getStatements

let Constructor (attributes:MemberAttributes)
                (arguments:CodeParameterDeclarationExpression list) 
                (baseTypeArguments:CodeExpression list)
                (statements:CodeStatement list) =
    let result = new CodeConstructor()
    result.Attributes <- attributes    
    result.Parameters.AddRange(arguments |> Array.ofList)
    result.BaseConstructorArgs.AddRange(baseTypeArguments |> Array.ofList)
    result.Statements.AddRange(statements |> Array.ofList)
    result |> AsMember

let ProtectedConstructor = Constructor MemberAttributes.Family
let PublicConstructor = Constructor MemberAttributes.Public

let CreateFunction (attributes:MemberAttributes) (name) (returnType:string) (parameters:CodeParameterDeclarationExpression list) (statements:CodeStatement list) =
    let result = new CodeMemberMethod(Name = name, 
                    Attributes = attributes,
                    ReturnType = new CodeTypeReference(returnType))
    result.Parameters.AddRange(parameters |> Array.ofList)                
    result.Statements.AddRange(statements |> Array.ofList)
    result |> AsMember

let CreatePublicFunction = CreateFunction (MemberAttributes.Public ||| MemberAttributes.Final)
let CreatePublicOverrideFunction = CreateFunction (MemberAttributes.Public ||| MemberAttributes.Override)
let CreateStaticFunction =CreateFunction  (MemberAttributes.Public ||| MemberAttributes.Static)

let Snippet snippet = new CodeSnippetTypeMember(snippet) |> AsMember
