//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSW.CodeGeneration.Units.Definitions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using WSW.CodeGeneration.Units.Definitions;
    
    
    public partial class Kilogram : MassValue
    {
        
        protected Kilogram(double value) : 
                base(value)
        {
        }
        
        public override string Suffix
        {
            get
            {
                return "kg";
            }
        }
        
protected override double Scalar => throw new NotImplementedException();
        
public static Kilogram FromKilogram(double value) => throw new NotImplementedException();
        
public static Kilogram FromSI(double siValue) => throw new NotImplementedException();
    }
}
