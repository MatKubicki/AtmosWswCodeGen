using System;
using System.Collections.Generic;
using System.Text;

namespace WSW.CodeGeneration.Units.Definitions
{
    public abstract class ValueWithUnit
    {
        protected readonly double _Value;

        public ValueWithUnit(double value)
        {
            _Value = value;
        }

        public abstract string Suffix { get; }        

        public override string ToString() => $"{_Value} {Suffix}";

        public abstract ValueWithUnit TryConvert(string unit);

        protected abstract double Scalar { get; }

        protected static double ConvertValueFromSI(double siValue, double scalar) => siValue / scalar;

        public double SIValue => _Value * Scalar;
    }
}
