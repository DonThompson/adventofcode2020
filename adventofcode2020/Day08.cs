using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace adventofcode2020
{
    class Op
    {
        public string instr;
        public int count;

        public Op(string input)
        {
            var parts = input.Split(' ');
            instr = parts[0];
            count = Int32.Parse(parts[1]);
        }
    }

    public class Day08
    {
        public static void DoDay08()
        {
            List<string> lines = File.ReadAllLines("input08.txt").ToList();

            Console.WriteLine($"Day 8a: {GetAccBeforeDupe(lines)}");
            Console.WriteLine($"Day 8b: {GetAccWithFlips(lines.ToArray())}");
        }

        private static int GetAccWithFlips(string[] lines)
        {
            //Iterate through all lines trying to flip jmp's and nop's
            for(int i = 0; i < lines.Length; i++)
            {
                Op op = new Op(lines[i]);
                if(op.instr == "jmp")
                {
                    //Try to flip
                    lines[i] = $"nop {op.count}";

                    //test
                    if(!HasDupe(lines, out int accValue))
                    {
                        //should have already 
                        return accValue;
                    }

                    //Flip back
                    lines[i] = $"jmp {op.count}";
                }
                else if(op.instr == "nop")
                {
                    //Try to flip
                    lines[i] = $"jmp {op.count}";

                    //test
                    if (!HasDupe(lines, out int accValue))
                    {
                        //should have already 
                        return accValue;
                    }

                    //Flip back
                    lines[i] = $"nop {op.count}";
                }
                else
                {
                    //don't touch acc'is
                }
            }

            //bad
            return -1;
        }

        private static bool HasDupe(string[] lines, out int accValue)
        {
            Dictionary<int, bool> map = new Dictionary<int, bool>();

            int acc = 0;
            int index = 0;
            while (index < lines.Length)
            {
                //Check if this instruction has executed
                if (map.ContainsKey(index))
                {
                    //It has a dupe!
                    accValue = acc;
                    return true;
                }
                else
                {
                    map.Add(index, true);
                }

                //Get the next operation
                Op op = new Op(lines[index]);
                switch (op.instr)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        acc += op.count;
                        index++;
                        break;
                    case "jmp":
                        index += op.count;
                        break;
                }
            }

            //No dupe!
            Console.WriteLine($"no dupe!  acc is {acc}");
            accValue = acc;
            return false;
        }

        //A
        private static int GetAccBeforeDupe(List<string> lines)
        {
            Dictionary<int, bool> map = new Dictionary<int, bool>();

            int acc = 0;
            int index = 0;
            while(true)
            {
                //Check if this instruction has executed
                if(map.ContainsKey(index))
                {
                    //It has!  We don't want to continue
                    return acc;
                }
                else
                {
                    map.Add(index, true);
                }

                //Get the next operation
                Op op = new Op(lines[index]);
                switch(op.instr)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        acc += op.count;
                        index++;
                        break;
                    case "jmp":
                        index += op.count;
                        break;
                }
            }
        }
    }
}
