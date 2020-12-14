using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day01
{
    public static class Day01
    {
        public static void DoDay01()
        {
            Console.WriteLine($"Day 1a:  {FindResultOf2()}");
            Console.WriteLine($"Day 1b:  {FindResultOf3()}");
        }

        private static int FindResultOf2()
        {
            const int target = 2020;

            List<string> input = File.ReadAllLines("input1.txt").ToList();

            for (int i = 0; i < input.Count; i++)
            {
                int first = Int32.Parse(input[i]);

                //Only need to test from our neighbor up, the below already tested us
                for (int j = i + 1; j < input.Count; j++)
                {
                    int second = Int32.Parse(input[j]);

                    if (first + second == target)
                    {
                        return first * second;
                    }
                }
            }

            return -1;
        }

        private static int FindResultOf3()
        {
            const int target = 2020;

            List<string> input = File.ReadAllLines("input1.txt").ToList();

            for (int i = 0; i < input.Count; i++)
            {
                int first = Int32.Parse(input[i]);

                //Only need to test from our neighbor up, the below already tested us
                for (int j = i + 1; j < input.Count; j++)
                {
                    int second = Int32.Parse(input[j]);

                    for (int k = j + 1; k < input.Count; k++)
                    {
                        int third = Int32.Parse(input[k]);

                        if (first + second + third == target)
                        {
                            return first * second * third;
                        }

                    }

                }
            }

            return -1;
        }
    }
}
