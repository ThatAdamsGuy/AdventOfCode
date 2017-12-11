using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> input = new List<int> { 197, 97, 204, 108, 1, 29, 5, 71, 0, 50, 2, 255, 248, 78, 254, 63 };
            List<int> list = new List<int>();

            for (int i = 0; i < 256; i++)
            {
                list.Add(i);
            }

            int currentPosition = 0;
            int tempPosition = 0;
            int skip = 0;
            while (input.Count() > 0)
            {
                Console.WriteLine("POS: " + currentPosition + ". SKIP: " + skip);
                Console.WriteLine("Loop");
                int position = input.First();
                tempPosition = currentPosition;
                List<int> tempList = new List<int>();
                for(int i = 0; i < input.First(); i++){
                    if(tempPosition >= 256)
                    {
                        tempPosition = 0;
                    }
                    tempList.Add(list[tempPosition]);
                    tempPosition++;
                }
                
                tempList.Reverse();
                for(int i = 0; i < input.First(); i++)
                {
                    if(currentPosition >= 256)
                    {
                        currentPosition = 0;
                    }
                    list[currentPosition] = tempList[i];
                    currentPosition++;
                }
                
                list.RemoveAt(0);
                skip++;
                currentPosition += skip;
            }

            Console.WriteLine("Sum: " + list[0] + " / " + list[1]);
            Console.ReadLine();
        }
    }
}
