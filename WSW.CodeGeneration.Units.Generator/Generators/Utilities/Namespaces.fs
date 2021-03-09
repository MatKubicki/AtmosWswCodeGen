module Namespaces
open System.CodeDom
open System

let private ResultNamespace = "WSW.CodeGeneration.Units.Definitions"

let private NamespacesToImport = [
    "System"
    "System.Collections.Generic"
    "System.ComponentModel"
    ResultNamespace
]

let private CreateNamespace targetNamespace =
    let resultNamespace = new CodeNamespace(targetNamespace)

    let namespaces = NamespacesToImport |> List.map (fun ns-> new CodeNamespaceImport(ns)) |> List.toArray
    resultNamespace.Imports.AddRange(namespaces)

    resultNamespace

let StripImportedNamespaceFromType (typ:Type) =
    if NamespacesToImport |> List.contains typ.Namespace then
        typ.Name
    else
        typ.FullName

let WrapInNamespaceAndUnit typeDeclaration =
    let compileUnit = new CodeCompileUnit()
    let resultNamespace = CreateNamespace ResultNamespace

    compileUnit.Namespaces.Add(resultNamespace) |> ignore
    resultNamespace.Types.Add(typeDeclaration) |> ignore

    compileUnit