module UnitGenerator
open WSW.CodeGeneration.Units.Generator

let CreateConstructor = 
    let valueParam = "value" |> Parameters.CreateParameter Types.DoubleType
    Members.ProtectedConstructor [valueParam.Declaration] [valueParam.ValueExpression] []

let CreateSuffixProperty (unit:UnitDefinition) = 
    Members.CreatePublicProperty "Suffix" Types.StringType [unit.Suffix |> Expressions.LiteralString |> Statements.MethodReturn]

//TODO: The exerise is to implement the generators for the property and two methods defined below.
//Note the current implementation of each generator is an unhelpful snippet that is just enough to get the code to build.

//List of Utility methods you'll need:
//Members.CreateProtectedProperty
//Members.CreateStaticFunction
//
//Parameters.CreateParameter
//Types.DoubleType
//
//Expressions.PrimitiveValue
//Expressions.CreateObject
//Expressions.CallMethod
//
//Statements.MethodReturn
  
let CreateScalarProperty (unit:UnitDefinition) = Members.Snippet "protected override double Scalar => throw new NotImplementedException();"
//TODO: Generate code somewhat like the following:
// Hint: Look at CreateSuffixProperty above as a starting point.  Try that and then see what other things are available in the Utilities folder
//protected override double Scalar
//{
//    get
//    {
//        return 0.0254D;
//    }
//}


let CreateFromUnitMethod (unit:UnitDefinition) = Members.Snippet (sprintf "public static %s From%s(double value) => throw new NotImplementedException();" unit.Name unit.Name)
//TODO Generate code somewhat like the following:
// Hint: Parameters.CreateParameter - will create both a parameter and an expression to reference it
// Look at the other things you can create in Members.xxxxx
//public static Inches FromInches(double value)
//{
//    return new Inches(value);
//}

let CreateFromSiMethod (unit:UnitDefinition) = Members.Snippet (sprintf "public static %s FromSI(double siValue) => throw new NotImplementedException();" unit.Name)
//TODO Generate code somewhat like the following:
// Hint: Notice the call to a method ConvertValueFromSI
//public static Inches FromSI(double siValue)
//{
//    return new Inches(ConvertValueFromSI(siValue, 0.0254D));
//}
    



let GenerateUnit (unitType) (unit:UnitDefinition) =
    let constructor = CreateConstructor
    let suffixProperty = unit |> CreateSuffixProperty
    let scalarProperty = unit |> CreateScalarProperty
    let createFromUnit = unit |> CreateFromUnitMethod
    let createFromSi = unit |> CreateFromSiMethod    

    let resultClass = Classes.CreatePublicClass (unit |> TypeNames.UnitClassName) 
                                                (unitType |> TypeNames.UnitTypeClassName |> Some)
                                                [
                                                     constructor
                                                     suffixProperty
                                                     scalarProperty
                                                     createFromUnit
                                                     createFromSi                                                        
                                                ]
    resultClass |> Namespaces.WrapInNamespaceAndUnit