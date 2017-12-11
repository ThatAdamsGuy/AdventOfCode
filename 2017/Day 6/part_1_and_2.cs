using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int finalHashCode = 0;
            int counter = 0;
            List<int> hashCodes = new List<int>();
            List<int> input = new List<int>{
                4,1,15,12,0,9,9,5,5,8,7,3,14,5,12,3
            };
            hashCodes.Add(getHash(input));

            while (true)
            {
                int iterator = 0;
                int location = 0;

                int highest = 0;
                int highestLocation = 0;
                for(int i = 0; i < 16; i++)
                {
                    if (input[i] > highest)
                    {
                        highestLocation = i;
                        highest = input[i];
                    }
                }

                location = highestLocation + 1;
                if(location == 16)
                {
                    location = 0;
                }
                iterator = highest;
                input[highestLocation] = 0;

                while(iterator != 0)
                {
                    input[location]++;
                    iterator--;
                    location++;
                    if(location == 16)
                    {
                        location = 0;
                    }
                }

                /*
                foreach(int loc in input)
                {
                    Console.Write(loc + " ");
                }
                Console.WriteLine();
                Console.WriteLine(getHash(input));
                
                */
                
                counter++;
                if (hashCodes.Contains(getHash(input)))
                {
                    finalHashCode = getHash(input);
                    break;
                } else
                {
                    hashCodes.Add(getHash(input));
                }



                //break;
            }
            int loop = hashCodes.IndexOf(finalHashCode);

            Console.WriteLine("COUNT: " + counter);
            Console.WriteLine("LOOP : " + (counter - loop));
            Console.ReadLine();
        }

        private static int getHash(List<int> input)
        {
            string hash = null;
            foreach(int loc in input)
            {
                hash += loc.ToString() + ",";
            }


            return hash.GetHashCode();
        }


    }
}
