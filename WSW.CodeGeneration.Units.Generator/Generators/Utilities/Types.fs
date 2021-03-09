module Types
open System.CodeDom
open System.Collections.Generic

let DoubleType = new CodeTypeReference(typedefof<double>)
let StringType = new CodeTypeReference(typedefof<string>)

let IEnumerableOfStringsType =     
    let result = new CodeTypeReference(typedefof<IEnumerable<_>>)
    result.TypeArguments.Add(StringType) |> ignore
    result
