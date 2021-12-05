using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    class Day07
    {
        public static void Run()
        {
            int valid = 0;
            var inputs = File.ReadAllLines("day07Input.txt");

            foreach(var input in inputs)
            {
                List<string> hypernets = new List<string>();
                List<string> notHypernets = new List<string>();

                List<string> xyx = new List<string>();
                List<string> yxy = new List<string>();

                string curState = input;

                while (true)
                {
                    if (!curState.Contains('[') && !string.IsNullOrEmpty(curState))
                    {
                        notHypernets.Add(curState);
                        break;
                    }
                    var tmpInput = curState.Split(new[] { '[' }, 2).ToList();
                    notHypernets.Add(tmpInput.First());
                    tmpInput = tmpInput[1].Split(new[] { ']' }, 2).ToList();
                    hypernets.Add(tmpInput.First());
                    curState = tmpInput[1];
                }

                bool isInvalid = false;
                foreach (string item in hypernets)
                {
                    for (int i = 0; i < item.Length - 4; i++)
                    {
                        if (item[i + 1] == item[i + 2] && item[i] == item[i + 3] && item[i] != item[i + 1])
                        {
                            //ABBA in middle bit, invalid
                            isInvalid = true;
                            break;
                        }
                    }
                    if (isInvalid) break;
                }
                if (isInvalid) continue;

                bool isValid = false;
                foreach (string item in notHypernets)
                {
                    for (int i = 0; i < item.Length - 4; i++)
                    {
                        if (item[i + 1] == item[i + 2] && item[i] == item[i + 3] && item[i] != item[i + 1])
                        {
                            //ABBA in outside bit only, valid
                            isValid = true;
                            break;
                        }
                    }
                    if (isValid) break;
                }
                if (isValid)
                {
                    valid++;
                    continue;
                }
            }
        }
    }
}
