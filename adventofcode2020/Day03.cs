using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace adventofcode2020
{
    public class Day03
    {
        public static void DoDay03()
        {
            bool[,] map = MapInput();
            Console.WriteLine($"Day 3a:  {CountTreesOnSlope(map, 3, 1)}");

            //b
            uint val1 = CountTreesOnSlope(map, 1, 1);
            uint val2 = CountTreesOnSlope(map, 3, 1);
            uint val3 = CountTreesOnSlope(map, 5, 1);
            uint val4 = CountTreesOnSlope(map, 7, 1);
            uint val5 = CountTreesOnSlope(map, 1, 2);
            uint result = val1 * val2 * val3 * val4 * val5;
            Console.WriteLine($"Day 3b:  {result}");
        }

        private static uint CountTreesOnSlope(bool[,] map, int incX, int incY)
        {
            uint treesEncountered = 0;
            //Starting position
            int x = 0, y = 0;
            //Inc first move
            x += incX;
            y += incY;

            int mapHeight = map.GetLength(0);
            int mapWidth = map.GetLength(1);
            while(y < mapHeight)
            {
                //Is it off the right edge?  if so we need to wrap
                if (x >= mapWidth)
                    x -= mapWidth;

                //Is it a tree?
                if (map[y, x])
                    treesEncountered++;

                //Move to next position in the slope
                x += incX;
                y += incY;
            }

            return treesEncountered;
        }

        private static bool[,] MapInput()
        {
            //Hardcoded based on input
            bool[,] map = new bool[323, 31];

            int i = 0;
            foreach(string line in File.ReadAllLines("input03.txt"))
            {
                int j = 0;
                foreach(char c in line)
                {
                    if (c == '.')
                        map[i,j] = false;
                    else
                        map[i,j] = true;
                    j++;
                }
                i++;
            }

            return map;
        }
    }
}
