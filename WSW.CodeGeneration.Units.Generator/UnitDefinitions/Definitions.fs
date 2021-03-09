module Definitions
open WSW.CodeGeneration.Units.Generator
open Helpers

let GetDefinitions = 
    [
        UnitType "Length" 
            [
                Unit "Meters" 1.0 "m"
                Unit "Inches" 0.0254 "in"
            ]
        UnitType "Volume" 
            [
                Unit "CubicMeters" 1.0 "m3"
                Unit "Liters" 0.001 "L"
                Unit "USGallons" 0.00378541 "USG"
            ]
        UnitType "Mass" 
            [
                Unit "Grams" 1.0 "g"
                Unit "Kilogram" 1000.0 "kg"
                Unit "USTon" 907185.0 "ton"
                Unit "Stone" 6350.29 "st"
            ]
    ]