using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventOfCode2015
{
    class Day12
    {
        public static void Run()
        {
            var json = File.ReadAllText("day12input.txt");

            // Parse JSON into a JObject (for objects) or JToken (for any JSON)
            JToken jsonObject = JToken.Parse(json);

            Console.WriteLine("Part 1: " + SumAllNumbers(jsonObject));
            Console.WriteLine("Part 2: " + SumAllNumbersIgnoreRed(jsonObject));
        }

        private static int SumAllNumbers(JToken token)
        {
            int sum = 0;

            switch (token.Type)
            {
                case JTokenType.Integer:
                    sum += token.Value<int>();
                    break;

                case JTokenType.Array:
                    foreach (var item in token.Children())
                    {
                        sum += SumAllNumbers(item);
                    }
                    break;

                case JTokenType.Object:
                    foreach (var property in token.Children<JProperty>())
                    {
                        sum += SumAllNumbers(property.Value);
                    }
                    break;

                // For other types (strings, booleans, null), we don't add anything
                default:
                    break;
            }

            return sum;
        }

        private static int SumAllNumbersIgnoreRed(JToken token)
        {
            int sum = 0;
            switch (token.Type)
            {
                case JTokenType.Integer:
                    sum += token.Value<int>();
                    break;

                case JTokenType.Array:
                    foreach (var item in token.Children())
                    {
                        sum += SumAllNumbersIgnoreRed(item);
                    }
                    break;

                case JTokenType.Object:
                    // Check if any property has the value "red"
                    bool hasRedValue = token.Children<JProperty>()
                        .Any(prop => prop.Value.Type == JTokenType.String && prop.Value.Value<string>() == "red");

                    // If no "red" value found, process all properties
                    if (!hasRedValue)
                    {
                        foreach (var property in token.Children<JProperty>())
                        {
                            sum += SumAllNumbersIgnoreRed(property.Value);
                        }
                    }
                    // If "red" value found, ignore this entire object (return 0)
                    break;

                // For other types (strings, booleans, null), we don't add anything
                default:
                    break;
            }

            return sum;
        }
    }
}
