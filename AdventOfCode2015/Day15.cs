using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day15
    {
        public static void Run()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient("Frosting", 4, -2, 0, 0, 5),
                new Ingredient("Candy", 0, 5, -1, 0, 8),
                new Ingredient("Butterscotch", -1, 0, 5, 0, 6),
                new Ingredient("Sugar", 0, 0, -2, 2, 1)
            };

            int highestScore = int.MinValue;
            int highestScoreWith500Calories = int.MinValue;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100 - i; j++)
                {
                    for (int k = 0; k < 100 - i - j; k++)
                    {
                        int l = 100 - i - j - k;
                        if(i+j+k+l != 100)
                        {
                            throw new Exception("Maths is broken");
                        }
                        int score = CalculateScore(ingredients, i, j, k, l);
                        highestScore = Math.Max(highestScore, score);
                        if(CalculateCalories(ingredients, i, j, k, l) == 500)
                        {
                            highestScoreWith500Calories = Math.Max(highestScoreWith500Calories, score);
                        }
                    }
                }
            }
            Console.WriteLine("Part 1 - " + highestScore);
            Console.WriteLine("Part 2 - " + highestScoreWith500Calories);
        }

        private static int CalculateCalories(List<Ingredient> ingredients, int a, int b, int c, int d)
        {
            return ingredients[0].Calories * a + ingredients[1].Calories * b + ingredients[2].Calories * c + ingredients[3].Calories * d;
        }

        private static int CalculateScore(List<Ingredient> ingredients, int a, int b, int c, int d)
        {
            int capacity = Math.Max(0, ingredients[0].Capacity * a + ingredients[1].Capacity * b + ingredients[2].Capacity * c + ingredients[3].Capacity * d);
            int durability = Math.Max(0, ingredients[0].Durability * a + ingredients[1].Durability * b + ingredients[2].Durability * c + ingredients[3].Durability * d);
            int flavor = Math.Max(0, ingredients[0].Flavor * a + ingredients[1].Flavor * b + ingredients[2].Flavor * c + ingredients[3].Flavor * d);
            int texture = Math.Max(0, ingredients[0].Texture * a + ingredients[1].Texture * b + ingredients[2].Texture * c + ingredients[3].Texture * d);
            return capacity * durability * flavor * texture;
        }

        private class Ingredient
        {
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int Durability { get; set; }
            public int Flavor { get; set; }
            public int Texture { get; set; }
            public int Calories { get; set; }

            public Ingredient(string name, int capacity, int durability, int flavor, int texture, int calories)
            {
                Name = name;
                Capacity = capacity;
                Durability = durability;
                Flavor = flavor;
                Texture = texture;
                Calories = calories;
            }
        }
    }
}
