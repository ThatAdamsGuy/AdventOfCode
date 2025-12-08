using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    enum ItemCategory
    {
        Weapon,
        Armour,
        Ring
    }

    class Day21
    {
        // Boss Stats
        private static int bossStartingHp = 104;
        private static int bossDamage = 8;
        private static int bossArmour = 1;

        private static int playerStartingHp = 100;

        private static List<Item> Shop { get; set; }

        public static void Run()
        {
            Shop = PopulateShop();

            int winningCostGold = int.MaxValue;
            int losingCostGold = int.MinValue;

            var weapons = Shop.Where(x => x.Category == ItemCategory.Weapon).ToList();
            var armours = Shop.Where(x => x.Category == ItemCategory.Armour).ToList();
            var rings = Shop.Where(x => x.Category == ItemCategory.Ring).ToList();
            foreach (var weapon in weapons)
            {
                foreach (var armour in armours)
                {
                    // All combinations of two rings (including same ring twice)
                    for (int i = 0; i < rings.Count; i++)
                    {
                        for (int j = i; j < rings.Count; j++)
                        {
                            var ring1 = rings[i];
                            var ring2 = rings[j];

                            // Skip if same ring (unless it's "No Ring")
                            if (i == j && ring1.Name != "No Ring")
                                continue;
                            int cost = weapon.Cost + armour.Cost + ring1.Cost + ring2.Cost;

                            bool winFight = WinFight(weapon.Damage + ring1.Damage + ring2.Damage, armour.Armour + ring1.Armour + ring2.Armour);
                            if (winFight && cost < winningCostGold)
                            {
                                winningCostGold = cost;
                            }
                            if (!winFight && cost > losingCostGold)
                            {
                                losingCostGold = cost;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Part 1 - " + winningCostGold);
            Console.WriteLine("Part 2 - " + losingCostGold);
        }

        private static bool WinFight(int playerDamage, int playerArmour)
        {
            int playerHealth = playerStartingHp;
            int bossHealth = bossStartingHp;
            bool playerTurn = true;

            do
            {
                if (playerTurn)
                {
                    bossHealth -= Math.Max(1, (playerDamage - bossArmour));
                }
                else
                {
                    playerHealth -= Math.Max(1, (bossDamage - playerArmour));
                }
                playerTurn = !playerTurn;
            }
            while (playerHealth > 0 && bossHealth > 0);
            return playerHealth > 0;
        }


        private static List<Item> PopulateShop()
        {
            return new List<Item>
            {
                new Item { Name = "Dagger", Category = ItemCategory.Weapon, Cost = 8, Damage = 4, Armour = 0 },
                new Item { Name = "Shortsword", Category = ItemCategory.Weapon, Cost = 10, Damage = 5, Armour = 0 },
                new Item { Name = "Warhammer", Category = ItemCategory.Weapon, Cost = 25, Damage = 6, Armour = 0 },
                new Item { Name = "Longsword", Category = ItemCategory.Weapon, Cost = 40, Damage = 7, Armour = 0 },
                new Item { Name = "Greataxe", Category = ItemCategory.Weapon, Cost = 74, Damage = 8, Armour = 0 },

                new Item { Name = "No Armour", Category = ItemCategory.Armour, Cost = 0, Damage = 0, Armour = 0 },
                new Item { Name = "Leather", Category = ItemCategory.Armour, Cost = 13, Damage = 0, Armour = 1 },
                new Item { Name = "Chainmail", Category = ItemCategory.Armour, Cost = 31, Damage = 0, Armour = 2 },
                new Item { Name = "Splintmail", Category = ItemCategory.Armour, Cost = 53, Damage = 0, Armour = 3 },
                new Item { Name = "Bandedmail", Category = ItemCategory.Armour, Cost = 75, Damage = 0, Armour = 4 },
                new Item { Name = "Platemail", Category = ItemCategory.Armour, Cost = 102, Damage = 0, Armour = 5 },

                new Item { Name = "No Ring", Category = ItemCategory.Ring, Cost = 0, Damage = 0, Armour = 0 },
                new Item { Name = "Damage +1", Category = ItemCategory.Ring, Cost = 25, Damage = 1, Armour = 0 },
                new Item { Name = "Damage +2", Category = ItemCategory.Ring, Cost = 50, Damage = 2, Armour = 0 },
                new Item { Name = "Damage +3", Category = ItemCategory.Ring, Cost = 100, Damage = 3, Armour = 0 },
                new Item { Name = "Defence +1", Category = ItemCategory.Ring, Cost = 20, Damage = 0, Armour = 1 },
                new Item { Name = "Defence +2", Category = ItemCategory.Ring, Cost = 40, Damage = 0, Armour = 2 },
                new Item { Name = "Defence +3", Category = ItemCategory.Ring, Cost = 80, Damage = 0, Armour = 3 },
            };
        }

        class Item
        {
            public string Name { get; set; }
            public ItemCategory Category { get; set; }
            public int Cost { get; set; }
            public int Damage { get; set; }
            public int Armour { get; set; }

        }
    }
}
