using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._1
{
    class Program
    {
        static int Day3SpiralMemoryPart1(int n)
        {
            if (n == 1) return 0;

            var x = 0;
            var y = 0;

            var stepCount = 1; // Initial step amount.
            var stepCountChange = true; // Change when true.
            var direction = 0; // right, up, left, down

            // Get the x,y coordinate for each step of i. 
            for (var i = 1; ;)
            {
                for (var j = 0; j < stepCount; j += 1)
                {
                    // Take a step
                    switch (direction)
                    {
                        case 0:
                            // right
                            x += 1;
                            break;
                        case 1:
                            // up
                            y += 1;
                            break;

                        case 2:
                            // left
                            x -= 1;
                            break;

                        case 3:
                            // down
                            y -= 1;
                            break;
                        default:
                            break;
                    }

                    // Needs to be incremented here after we take a step.
                    // Then we check the outer loop condition here, and so then jump out if needed.
                    // The ghost of Djikstra will probably haunt me for a bit now...~
                    i += 1;

                    if (i == n) goto EndOfLoop;
                }

                direction = (direction + 1) % 4;
                stepCountChange = !stepCountChange;
                if (stepCountChange) stepCount += 1;
            }
            EndOfLoop:
            var l1distance = Math.Abs(x) + Math.Abs(y);

            System.Diagnostics.Debug.WriteLine("f({0}) = ({1},{2}), |f({0})| = {3}", n, x, y, l1distance);

            return l1distance;
        }

        static int Day3SpiralMemoryPart2(int n)
        {
            if (n == 1) return 1;

            var x = 0;
            var y = 0;

            var stepCount = 1; // Initial step amount.
            var stepCountChange = true; // Change when true.
            var direction = 0;
            var values = new Dictionary<string, int>();

            values["0,0"] = 1;

            for (; ; )
            {
                for (var j = 0; j < stepCount; j += 1)
                {
                    // Take a step
                    switch (direction)
                    {
                        case 0:
                            // right
                            x += 1;
                            break;
                        case 1:
                            // up
                            y += 1;
                            break;

                        case 2:
                            // left
                            x -= 1;
                            break;

                        case 3:
                            // down
                            y -= 1;
                            break;
                        default:
                            break;
                    }

                    // Determine sum of neighbours for value of current location.
                    var sum = 0;
                    var val = 0;

                    if (values.TryGetValue(string.Format("{0},{1}", x + 1, y), out val)) sum += val;
                    if (values.TryGetValue(string.Format("{0},{1}", x + 1, y + 1), out val)) sum += val;
                    if (values.TryGetValue(string.Format("{0},{1}", x, y + 1), out val)) sum += val;
                    if (values.TryGetValue(string.Format("{0},{1}", x - 1, y + 1), out val)) sum += val;
                    if (values.TryGetValue(string.Format("{0},{1}", x - 1, y), out val)) sum += val;
                    if (values.TryGetValue(string.Format("{0},{1}", x - 1, y - 1), out val)) sum += val;
                    if (values.TryGetValue(string.Format("{0},{1}", x, y - 1), out val)) sum += val;
                    if (values.TryGetValue(string.Format("{0},{1}", x + 1, y - 1), out val)) sum += val;

                    // Check here to see if the sum exceeds our input. Otherwise, store the sum computed and continue.
                    if (sum > n) return sum;
                    values[string.Format("{0},{1}", x, y)] = sum;
                }

                direction = (direction + 1) % 4;
                stepCountChange = !stepCountChange;
                if (stepCountChange) stepCount += 1;
            }
        }

        static void Main(string[] args)
        {
            var day3PuzzleInput = 312051;

            Console.WriteLine(Day3SpiralMemoryPart1(day3PuzzleInput));
            Console.WriteLine(Day3SpiralMemoryPart2(day3PuzzleInput));

            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }
    }
}
