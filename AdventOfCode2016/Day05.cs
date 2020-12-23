using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace AdventOfCode2016
{
    class Day05
    {
        public static readonly string input = "reyedfim";
        public static void Run()
        {
            int counter = 0;
            string password = "";
            string secondPassword = "xxxxxxxx";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan firstTs = stopWatch.Elapsed;
            bool timespanFound = false;

            while (true)
            {
                string toCheck = input + counter.ToString();
                MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
                var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(toCheck));
                StringBuilder hash = new StringBuilder();
                foreach(var item in bytes)
                {
                    hash.Append(item.ToString("x2"));
                }

                string result = hash.ToString();
                if (result.Substring(0, 5).Equals("00000")) {
                    if (password.Length < 8)
                    {
                        password += result[5];
                    } else
                    {
                        if (!timespanFound)
                        {
                            firstTs = stopWatch.Elapsed;
                            timespanFound = true;
                        }
                    }


                    if(int.TryParse(result[5].ToString(), out int pos) && pos >= 0 && pos <= 7 && secondPassword[pos] == 'x')
                    {
                        StringBuilder second = new StringBuilder(secondPassword);
                        second[pos] = result[6];
                        secondPassword = second.ToString();
                    }
                    Console.WriteLine(password);
                    Console.WriteLine(secondPassword);
                    if (!secondPassword.Any(x => x == 'x')) break;
                } 
                counter++;
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                firstTs.Hours, firstTs.Minutes, firstTs.Seconds,
                firstTs.Milliseconds / 10);
            string secondElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine($"First Password:  {password} (elapsed time: {elapsedTime})");
            Console.WriteLine($"Second Password: {secondPassword} (elapsed time: {secondElapsedTime})");
        }
    }
}
