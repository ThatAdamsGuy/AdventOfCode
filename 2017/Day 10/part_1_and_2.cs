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
            List<byte> inputASCII = ASCIIEncoding.ASCII.GetBytes("197,97,204,108,1,29,5,71,0,50,2,255,248,78,254,63").ToList();
            inputASCII.AddRange(new List<byte>() { 17, 31, 73, 47, 23 });

            //List<int> input = new List<int> { 197, 97, 204, 108, 1, 29, 5, 71, 0, 50, 2, 255, 248, 78, 254, 63 };
            List<int> list = new List<int>();

            for (int i = 0; i < 256; i++)
            {
                list.Add(i);
            }

            int currentPosition = 0;
            int skip = 0;
            for(int round = 0; round < 64; round++)
            {
                foreach(byte hashV in inputASCII)
                {
                    Console.WriteLine("POS: " + currentPosition + ". SKIP: " + skip);
                    Console.WriteLine("Loop");
                    List<int> tempList = new List<int>();
                    for(int i = 0; i < hashV; i++){
                        tempList.Add(list[(currentPosition + i) % 256]);
                    }
                
                    tempList.Reverse();
                    for(int i = 0; i < hashV; i++)
                    {
                        list[currentPosition % 256] = tempList[i];
                        currentPosition++;
                    }
                
                    currentPosition += skip;
                    skip++;
                }
            }

            string output = string.Empty;
            for(int i=0; i<16; i++)
            {
                int hashyGoodness = list[i * 16];
                for (int j=1; j<16; j++)
                {
                    hashyGoodness = hashyGoodness ^ list[i * 16 + j];
                }

                if (hashyGoodness.ToString("x").Length == 1)
                {
                    output += "0";
                }

                output += hashyGoodness.ToString("x");
            }

            Console.WriteLine("HahaHash: " + output + " length: " + output.Length);
            Console.WriteLine("Sum: " + list[0] * list[1]);
            Console.ReadLine();
        }
    }
}
