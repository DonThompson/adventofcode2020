using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace adventofcode2020
{
    class Rule
    {
        public string outerBag;
        public List<(string, int)> contains = new List<(string, int)>();

        public bool CanHold(string targetBag)
        {
            foreach((string s, int cnt) in contains)
            {
                if (s == targetBag)
                    return true;
            }
            return false;
        }

        public Rule(string line)
        {
            //posh crimson bags contain 2 mirrored tan bags, 1 faded red bag, 1 striped gray bag.
            var parts = line.Split(" contain ");
            //posh crimson bags -- take off the s
            outerBag = parts[0].TrimEnd('s');

            //2 mirrored tan bags, 1 faded red bag, 1 striped gray bag.
            //1 dark silver bag.
            string contents = parts[1];
            foreach(var bag in contents.Split(","))
            {
                //2 mirrored tan bags
                //1 faded red bag
                string phrase = bag.TrimStart().TrimEnd('.');

                //Special - add nothing
                if (phrase == "no other bags")
                    continue;

                int firstSpace = phrase.IndexOf(' ');
                int cnt = Int32.Parse(phrase.Substring(0, firstSpace));
                string bagType = phrase.Substring(firstSpace + 1);
                if(cnt > 1)
                {
                    //remove the trailing s
                    bagType = bagType.TrimEnd('s');
                }

                contains.Add((bagType, cnt));
            }
            
        }
    }


    public class Day07
    {
        public static void DoDay07()
        {
            Dictionary<string, Rule> map = ParseInput();

            Console.WriteLine($"Day 7a: {CountWhichCanHold("shiny gold bag", map)}");
            Console.WriteLine($"Day 7b: {CountBagsInside("shiny gold bag", map)}");

        }

        //B
        private static int CountBagsInside(string targetBag, Dictionary<string, Rule> map)
        {
            int bagsInside = 0;

            Rule r = map[targetBag];
            foreach((string bagType, int cnt) in r.contains)
            {
                //cnt of bagType
                bagsInside += cnt;
                //Plus however many bags are inside those bags
                bagsInside += cnt * CountBagsInside(bagType, map);
            }
            return bagsInside;
        }



        //A
        private static int CountWhichCanHold(string targetBag, Dictionary<string, Rule> map)
        {
            //Work in reverse.  First, get a list of every color that can directly hold our target
            List<string> allAllowedBags = BagsWhichCanHoldDirectly(targetBag, map);

            List<string> nextRoundOfBags = new List<string>();
            nextRoundOfBags.AddRange(allAllowedBags);

            do
            {
                //Setup a set of bags to be further tested with those
                List<string> bagsToTest = new List<string>();
                bagsToTest.AddRange(nextRoundOfBags);
                nextRoundOfBags.Clear();

                //Now find all bags that can hold any of those bags
                foreach (string bag in bagsToTest)
                {
                    var moreBags = BagsWhichCanHoldDirectly(bag, map);
                    //Remove any dupes
                    foreach (string newBag in moreBags)
                    {
                        if (!allAllowedBags.Contains(newBag))
                        {
                            //Add to our entire array of allowed bags
                            allAllowedBags.Add(newBag);
                            //This bag now needs tested next iteration
                            nextRoundOfBags.Add(newBag);
                        }
                    }
                }
            } while (nextRoundOfBags.Count > 0);

            return allAllowedBags.Count;
        }

        //A
        private static List<string> BagsWhichCanHoldDirectly(string targetBag, Dictionary<string, Rule> map)
        {
            List<string> successes = new List<string>();
            foreach (KeyValuePair<string, Rule> kvp in map)
            {
                if (kvp.Value.CanHold(targetBag))
                    successes.Add(kvp.Key);
            }
            return successes;
        }

        //Setup
        private static Dictionary<string, Rule> ParseInput()
        {
            Dictionary<string, Rule> map = new Dictionary<string, Rule>();

            List<Rule> rules = new List<Rule>();
            foreach (string line in File.ReadAllLines("input07.txt"))
                rules.Add(new Rule(line));

            foreach(Rule r in rules)
            {
                map.Add(r.outerBag, r);
            }

            return map;
        }
    }
}
