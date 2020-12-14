using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace adventofcode2020
{
    class Group
    {
        public List<string> persons = new List<string>();

        public string GetAllAnswersCombined()
        {
            string answers = "";
            foreach(string p in persons)
            {
                answers += p;
            }
            return answers;
        }

        public int UniqueChars()
        {
            string allAnswers = GetAllAnswersCombined();
            string distinctAnswers = new string(allAnswers.Distinct().ToArray());
            return distinctAnswers.Length;
        }

        internal int UniqueGroupAnswers()
        {
            //If only 1 person, that's it
            if (persons.Count == 1)
                return persons[0].Length;

            //start with first person then remove
            string allAnswers = persons[0];
            for(int i = 1; i < persons.Count; i++)
            {
                //Just simply check them 1 by 1
                string newAnswers = "";
                string nextPerson = persons[i];
                foreach(char c in allAnswers)
                {
                    if (nextPerson.Contains(c))
                        newAnswers += c;                        
                }
                allAnswers = newAnswers;
            }

            return allAnswers.Length;
        }
    }

    public class Day06
    {
        public static void DoDay06()
        {
            List<Group> groups = ParseInput();

            Console.WriteLine($"Day 6a: {SumUniqueAnswers(groups)}");
            Console.WriteLine($"Day 6b: {SumGroupAnswers(groups)}");
        }

        private static object SumGroupAnswers(List<Group> groups)
        {
            int sum = 0;
            foreach (Group g in groups)
            {
                sum += g.UniqueGroupAnswers();
            }
            return sum;
        }

        private static object SumUniqueAnswers(List<Group> groups)
        {
            int sum = 0;
            foreach(Group g in groups)
            {
                sum += g.UniqueChars();
            }
            return sum;
        }

        private static List<Group> ParseInput()
        {
            List<Group> groups = new List<Group>();
            Group current = new Group();

            foreach (string line in File.ReadAllLines("input06.txt"))
            {
                if(string.IsNullOrEmpty(line))
                {
                    //end this group
                    groups.Add(current);
                    current = new Group();
                }
                else
                {
                    //new person
                    current.persons.Add(line);
                }
            }
            //Don't forget the last group
            groups.Add(current);

            return groups;
        }

    }
}
