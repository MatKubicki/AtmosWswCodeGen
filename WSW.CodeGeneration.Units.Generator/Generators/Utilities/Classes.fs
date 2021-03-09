module Classes

open System.CodeDom
open System.Reflection

let CreateClass (attributes:TypeAttributes) className (baseTypeName:string option) (members:CodeTypeMember List) =
    let resultClass = new CodeTypeDeclaration(className, IsClass = true, TypeAttributes = attributes, IsPartial = true)
    resultClass.BaseTypes.AddRange(baseTypeName |> Option.map (fun b-> new CodeTypeReference(b)) |> Option.toArray)
    resultClass.Members.AddRange(members |> Array.ofList)
    resultClass

let CreatePublicClass = CreateClass TypeAttributes.Public
let CreateAbstractClass = CreateClass (TypeAttributes.Public ||| TypeAttributes.Abstract)