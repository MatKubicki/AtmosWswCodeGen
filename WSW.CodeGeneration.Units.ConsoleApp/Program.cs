using System;
using System.Collections.Generic;
using System.Linq;
using WSW.CodeGeneration.Units.Definitions;

namespace WSW.CodeGeneration.Units.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Atmos Units");

            bool exit = false;

            while (!exit)
            {
                string unitType = SelectFromListOfStrings(UnitLibrary.UnitTypeNames, "Select a unit type:");
                string unit = SelectFromListOfStrings(UnitLibrary.TryGetUnits(unitType), $"Select a starting {unitType} unit:");

                Console.WriteLine($"Enter a value in {unit}: ");
                var value = double.Parse(Console.ReadLine());
                var valueAndUnit = UnitLibrary.TryCreate(unitType, value, unit);
                if (valueAndUnit != null)
                {
                    Console.WriteLine("Conversions:");
                    WriteLines(UnitLibrary.TryGetUnits(unitType).Select((u) => $"{valueAndUnit} = {valueAndUnit.TryConvert(u)}"));
                }
                else
                {
                    Console.WriteLine("Unknown unit");
                }

                Console.WriteLine("Start again [Y/N]");
                exit = Console.ReadLine().ToUpper() != "Y";
            }
        }

        private static string SelectFromListOfStrings(IEnumerable<string> options, string prompt)
        {
            List<string> indexedOptions = options.ToList();

            Console.WriteLine(prompt);
            WriteLines(indexedOptions.Select((u, i) => $"{i + 1}: {u}"));

            var index = int.Parse(Console.ReadLine()) - 1;
            while (index >= indexedOptions.Count)
            {
                Console.WriteLine("Out of range, try again");
                index = int.Parse(Console.ReadLine());
            }

            return indexedOptions[index];
        }

        private static void WriteLines(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
