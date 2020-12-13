﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day01
{
    class Program
    {
        private static int FindResult()
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

        static void Main(string[] args)
        {
            Console.WriteLine($"Day 1a:  {FindResult()}");
        }
    }
}