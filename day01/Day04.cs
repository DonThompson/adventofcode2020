using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace adventofcode2020
{
    class Passport
    {
        //quick version
        public List<(string, string)> parts;

        public Passport()
        {
            parts = new List<(string, string)>();
        }

        internal bool IsValidHacked()
        {
            //If we have 8 parts, it's valid
            if (parts.Count == 8)
                return true;

            //Otherwise, if we have 7 but are missing 'cid', that's OK too
            if (parts.Count == 7 && parts.Count(p => p.Item1 == "cid") == 0)
                return true;

            //No go
            return false;
        }

        internal bool IsMostlyValid()
        {
            //Must pass previous criteria
            if (!IsValidHacked())
                return false;

            //New criteria!
            foreach((string, string) pair in parts)
            {
                switch(pair.Item1)
                {
                    case "byr":
                        {
                            if (pair.Item2.Length != 4)
                                return false;
                            int yr = Int32.Parse(pair.Item2);
                            if (yr < 1920 || yr > 2002)
                                return false;
                        }
                        break;
                    case "iyr":
                        {
                            if (pair.Item2.Length != 4)
                                return false;
                            int yr = Int32.Parse(pair.Item2);
                            if (yr < 2010 || yr > 2020)
                                return false;
                        }
                        break;
                    case "eyr":
                        {
                            if (pair.Item2.Length != 4)
                                return false;
                            int yr = Int32.Parse(pair.Item2);
                            if (yr < 2020 || yr > 2030)
                                return false;
                        }
                        break;
                    case "hgt":
                        {
                            string hgt = pair.Item2;
                            if (hgt.Length <= 3)
                                return false;
                            string type = hgt.Substring(hgt.Length - 2);
                            int len = Int32.Parse(hgt.Substring(0, hgt.Length - 2));
                            if (type == "cm")
                            {
                                if (len < 150 || len > 193)
                                    return false;
                            }
                            else if (type == "in")
                            {
                                if (len < 59 || len > 76)
                                    return false;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case "hcl":
                        {
                            string hcl = pair.Item2;
                            if(hcl.Length == 7 && hcl[0] == '#')
                            {
                                if (!hclDigitCheck(hcl[1]))
                                    return false;
                                if (!hclDigitCheck(hcl[2]))
                                    return false;
                                if (!hclDigitCheck(hcl[3]))
                                    return false;
                                if (!hclDigitCheck(hcl[4]))
                                    return false;
                                if (!hclDigitCheck(hcl[5]))
                                    return false;
                                if (!hclDigitCheck(hcl[6]))
                                    return false;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case "ecl":
                        {
                            string ecl = pair.Item2;
                            if (ecl == "amb" || ecl == "blu" || ecl == "brn" || ecl == "gry" || ecl == "grn" || ecl == "hzl" || ecl == "oth")
                            {
                                //ok
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case "pid":
                        if (pair.Item2.Length != 9)
                            return false;
                        break;
                    case "cid":
                        //Don't care
                        break;
                }
            }

            //All passed!
            return true;
        }

        //0-9 or a-f
        private bool hclDigitCheck(char v)
        {
            if (v >= '0' && v <= '9')
                return true;
            if (v >= 'a' && v <= 'f')
                return true;
            return false;
        }

        //public string byr { get; set; }
        //public string iyr { get; set; }
        //public string eyr { get; set; }
        //public string hgt { get; set; }
        //public string hcl { get; set; }
        //public string ecl { get; set; }
        //public string pid { get; set; }
        //public string cid { get; set; }
    }

    public class Day04
    {
        public static void DoDay04()
        {
            var passports = GetPassports();

            Console.WriteLine($"4a: {CountValidPassports(passports)}");
            Console.WriteLine($"4b: {CountMoreValidPassports(passports)}");
        }

        private static object CountValidPassports(List<Passport> passports)
        {
            int valid = 0;
            foreach(Passport p in passports)
            {
                if (p.IsValidHacked())
                    valid++;
            }
            return valid;
        }

        private static object CountMoreValidPassports(List<Passport> passports)
        {
            int valid = 0;
            foreach (Passport p in passports)
            {
                if (p.IsMostlyValid())
                    valid++;
            }
            return valid;
        }

        private static List<Passport> GetPassports()
        {
            List<Passport> passports = new List<Passport>();
            Passport current = new Passport();
            foreach (string line in File.ReadAllLines("input04.txt"))
            {
                if (string.IsNullOrEmpty(line))
                {
                    //Save this passport, start a new one
                    passports.Add(current);
                    current = new Passport();
                }
                else
                {
                    var parts = line.Split(new char[] { ' ', '\n' });
                    foreach(string part in parts)
                    {
                        //key:value
                        string[] kvp = part.Split(':');
                        current.parts.Add((kvp[0], kvp[1]));
                    }
                }
            }
            //add the last one too
            passports.Add(current);

            return passports;
        }
    }
}
