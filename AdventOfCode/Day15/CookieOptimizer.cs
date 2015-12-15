using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

namespace Day15
{
    class CookieOptimizer
    {
        class Ingredient
        {
            public string name;
            public int capacity;
            public int durability;
            public int flavor;
            public int texture;
            public int calories;

            public void Read(string input)
            {
                //Candy: capacity 0, durability -1, flavor 0, texture 5, calories 8

                String[] tokens = input.Split(' ');
                name = tokens[0].TrimEnd(':');
                capacity = Int32.Parse(tokens[2].TrimEnd(','));
                durability = Int32.Parse(tokens[4].TrimEnd(','));
                flavor = Int32.Parse(tokens[6].TrimEnd(','));
                texture = Int32.Parse(tokens[8].TrimEnd(','));
                calories = Int32.Parse(tokens[10].TrimEnd(','));
            }
        }

        private List<Ingredient> ingredients;

        private int ComputeScore(int[] amounts)
        {
            int capacity = 0;
            int durability = 0;
            int flavor = 0;
            int texture = 0;
            for (int i = 0; i < ingredients.Count; i++)
            {
                capacity += ingredients[i].capacity * amounts[i];
                durability += ingredients[i].durability * amounts[i];
                flavor += ingredients[i].flavor * amounts[i];
                texture += ingredients[i].texture * amounts[i];
            }

            if (capacity < 0 || durability < 0 || flavor < 0 || texture < 0)
                return 0;

            return capacity*durability*flavor*texture;
        }

        public void OptimizeCookies()
        {
			ingredients = new List<Ingredient>();
            foreach (String readLine in File.ReadLines("input.txt"))
            {
                Ingredient currIngredient = new Ingredient();
                currIngredient.Read(readLine);
                ingredients.Add(currIngredient);
            }

            int totalScore = ComputeScore(new[] { 25, 25, 25, 25 });


        }

    }
}
