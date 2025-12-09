using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day22
    {
        private const int bossHp = 71;
        private const int bossDamage = 10;
        
        public static void Run()
        {
            var result1 = FindMinMana(false);
            Console.WriteLine($"Part 1 - {result1}");
            
            var result2 = FindMinMana(true);
            Console.WriteLine($"Part 2 - {result2}");
        }

        static int FindMinMana(bool hardMode)
        {
            return SearchMinMana(50, 500, 71, 0, 0, 0, 0, hardMode, int.MaxValue);
        }

        static int SearchMinMana(int wizHP, int wizMana, int bossHP, int shieldTimer, int poisonTimer, int rechargeTimer, int manaSpent, bool hardMode, int bestSoFar)
        {
            // Pruning: if we've already spent too much, give up
            if (manaSpent >= bestSoFar) return int.MaxValue;

            // Hard mode: wizard loses 1 HP at start of turn
            if (hardMode)
            {
                wizHP -= 1;
                if (wizHP <= 0) return int.MaxValue; // Wizard dies
            }

            // Apply effects at start of wizard turn
            bool shielded = shieldTimer > 0;
            if (rechargeTimer > 0) wizMana += 101;
            if (poisonTimer > 0) bossHP -= 3;
            
            // Decrement timers
            if (shieldTimer > 0) shieldTimer--;
            if (poisonTimer > 0) poisonTimer--;
            if (rechargeTimer > 0) rechargeTimer--;

            // Check if boss died from effects
            if (bossHP <= 0) return manaSpent;

            int minMana = int.MaxValue;

            // Try each spell
            int[] spellCosts = { 53, 73, 113, 173, 229 }; // MM, Drain, Shield, Poison, Recharge
            string[] spellNames = { "MagicMissile", "Drain", "Shield", "Poison", "Recharge" };

            for (int spellIndex = 0; spellIndex < 5; spellIndex++)
            {
                int cost = spellCosts[spellIndex];
                
                // Can afford spell?
                if (wizMana < cost) continue;
                
                // Spell already active?
                if ((spellIndex == 2 && shieldTimer > 0) || // Shield
                    (spellIndex == 3 && poisonTimer > 0) || // Poison
                    (spellIndex == 4 && rechargeTimer > 0))   // Recharge
                    continue;

                // Cast spell - create new state
                int newWizHP = wizHP;
                int newWizMana = wizMana - cost;
                int newBossHP = bossHP;
                int newShieldTimer = shieldTimer;
                int newPoisonTimer = poisonTimer;
                int newRechargeTimer = rechargeTimer;
                int newManaSpent = manaSpent + cost;

                // Apply spell effects
                switch (spellIndex)
                {
                    case 0: // Magic Missile
                        newBossHP -= 4;
                        break;
                    case 1: // Drain
                        newBossHP -= 2;
                        newWizHP += 2;
                        break;
                    case 2: // Shield
                        newShieldTimer = 6;
                        break;
                    case 3: // Poison
                        newPoisonTimer = 6;
                        break;
                    case 4: // Recharge
                        newRechargeTimer = 5;
                        break;
                }

                // Check if boss died from spell
                if (newBossHP <= 0)
                {
                    minMana = Math.Min(minMana, newManaSpent);
                    continue;
                }

                // Boss turn - apply effects
                bool bossShielded = newShieldTimer > 0;
                if (newRechargeTimer > 0) newWizMana += 101;
                if (newPoisonTimer > 0) newBossHP -= 3;
                
                // Decrement timers
                if (newShieldTimer > 0) newShieldTimer--;
                if (newPoisonTimer > 0) newPoisonTimer--;
                if (newRechargeTimer > 0) newRechargeTimer--;

                // Check if boss died from effects
                if (newBossHP <= 0)
                {
                    minMana = Math.Min(minMana, newManaSpent);
                    continue;
                }

                // Boss attacks
                int damage = Math.Max(1, bossDamage - (bossShielded ? 7 : 0));
                newWizHP -= damage;

                // Check if wizard died
                if (newWizHP <= 0) continue;

                // Recurse to next turn
                int result = SearchMinMana(newWizHP, newWizMana, newBossHP, newShieldTimer, newPoisonTimer, newRechargeTimer, newManaSpent, hardMode, Math.Min(minMana, bestSoFar));
                minMana = Math.Min(minMana, result);
            }

            return minMana;
        }
    }
}
