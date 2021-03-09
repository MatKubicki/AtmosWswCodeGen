# F# Code Generation Exercise

## Overview
The example application is a simple unit conversion command line executable.  
You run the application it lists unit types (length, mass, velocity etc) the user selects one.  
It then list units of this type (km, meters, inches, feet), again the user selects one.  
Finally the user enters a value in their selected unit, and this value is converted to all other units of this type.

The solution contains three projects:
 - WSW.CodeGeneration.Units.Generator - The F# code generator that produces the definitions for the units
 - WSW.CodeGeneration.Units.Definitions - C# definitions for units, primarily generated code
 - WSW.CodeGeneration.Units.ConsoleApp - C# Console app that provides the user interface to the definitions.

## Exercise
The goal is to finish the implementation of the UnitGenerator module, this module creates the definition code for a single unit (e.g. Meters).  
It is currently only producing dummy implementations of various important methods, just enough to get the generated code to build.

## Steps

 1. Run the WSW.CodeGeneration.Units.Generator executable to see the current state of the generated code
 2. See how WSW.CodeGeneration.Units.Definitions is populated with lots of code, note how the classes for specific units are full of NotImplementedExceptions
 3. In WSW.CodeGeneration.Units.Generator open the file 'Exercises_UnitGenerator' 
 4. Implement the TODOs
 5. Keep re-running WSW.CodeGeneration.Units.Generator to see your progress.
 6. Run WSW.CodeGeneration.Units.ConsoleApp to see how your generated code functions
