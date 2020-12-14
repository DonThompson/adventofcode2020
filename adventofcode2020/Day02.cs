using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace adventofcode2020
{
    public class Day02
    {
        public static void DoDay02()
        {
            Console.WriteLine($"2a: {CountValidPasswords()}");
            Console.WriteLine($"2b: {CountValidWeirdPasswords()}");
        }

        private static int CountValidPasswords()
        {
            int countValid = 0;

            foreach(string line in File.ReadAllLines("input02.txt"))
            {
                string[] split = line.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                int min = Int32.Parse(split[0]);
                int max = Int32.Parse(split[1]);
                char letter = Char.Parse(split[2]);
                string fullPass = split[3];

                int countInPass = fullPass.Count(c => c == letter);
                if (countInPass >= min && countInPass <= max)
                    countValid++;
            }

            return countValid;
        }

        private static int CountValidWeirdPasswords()
        {
            int countValid = 0;

            foreach (string line in File.ReadAllLines("input02.txt"))
            {
                string[] split = line.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                int first = Int32.Parse(split[0]);
                int last = Int32.Parse(split[1]);
                char letter = Char.Parse(split[2]);
                string fullPass = split[3];

                bool isInFirst = fullPass[first - 1] == letter;
                bool isInLast = fullPass[last - 1] == letter;

                if ((isInFirst || isInLast) && isInFirst != isInLast)
                    countValid++;
            }

            return countValid;
        }
    }
}
