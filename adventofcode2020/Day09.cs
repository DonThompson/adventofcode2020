using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace adventofcode2020
{
    public class Day09
    {
        public static void DoDay09()
        {
            string[] lines = File.ReadAllLines("input09.txt");

            int weakness = FindWeakness(lines);
            Console.WriteLine($"Day 9a: {weakness}");
            Console.WriteLine($"Day 9b: {FindContiguousTry2(lines, (UInt64)weakness)}");
        }

        private static UInt64 FindContiguousTry2(string[] lines, UInt64 weakness)
        {
            string[] copy = new string[lines.Length];
            Array.Copy(lines, copy, lines.Length);

            for(int i = 0; i < lines.Length; i++)
            {
                //Starting from here, iterate through the copy until we either add up to the weakness or pass it
                UInt64 currentCount = 0;
                bool done = false;
                for(int j = i; j < lines.Length && !done; j++)
                {
                    currentCount += UInt64.Parse(lines[j]);

                    if(currentCount == weakness)
                    {
                        //WINNER
                        int len = j - i;
                        string[] range = new string[len];
                        Array.Copy(lines, i, range, 0, len);

                        UInt64 min = UInt64.MaxValue;
                        UInt64 max = 0;
                        foreach(string s in range)
                        {
                            UInt64 test = UInt64.Parse(s);
                            if (test > max)
                                max = test;
                            if (test < min)
                                min = test;
                        }

                        return min + max;
                    }
                    else if(currentCount > weakness)
                    {
                        //not our winner, give up
                        done = true;
                    }
                    else
                    {
                        //less, keep going
                    }
                }
            }

            //bad
            return 0;
        }

        //A
        private static int FindWeakness(string[] lines)
        {
            for(int i = 25; i < lines.Length; i++)
            {
                int num = Int32.Parse(lines[i]);
                string[] newLines = new string[25];
                Array.Copy(lines, i - 25, newLines, 0, 25);

                if(!HasSumInRange(num, newLines))
                {
                    return num;
                }
            }

            //bad
            return -1;
        }

        //A
        private static bool HasSumInRange(int num, string[] newLines)
        {
            foreach(string line in newLines)
            {
                int x = Int32.Parse(line);

                //Goal is x + y = num.
                //  Since we know num & x, reverse to num - x = y;
                int y = num - x;
                //Does it exist?  If so we've matched.
                if (!string.IsNullOrEmpty(newLines.ToList().Find(n => n == y.ToString())))
                    return true;
            }

            return false;
        }
    }
}
