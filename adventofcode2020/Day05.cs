using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace adventofcode2020
{
    class BinaryRow
    {
        int minRow = 0;
        int maxRow = 127;

        int minSeat = 0;
        int maxSeat = 7;

        public void FrontHalf()
        {
            maxRow = (minRow + maxRow) / 2;
        }

        public void BackHalf()
        {
            minRow = ((minRow + maxRow) / 2) + 1;
        }

        public int GetRow()
        {
            if (minRow != maxRow)
                throw new Exception("bad");
            return minRow;
        }

        public void LeftHalf()
        {
            maxSeat = (minSeat + maxSeat) / 2;
        }

        public void RightHalf()
        {
            minSeat = ((minSeat + maxSeat) / 2) + 1;
        }

        public int GetSeat()
        {
            if (minSeat != maxSeat)
                throw new Exception("bad");
            return minSeat;
        }
    }

    public class Day05
    {
        public static void DoDay05()
        {
            var seats = GetAllUniqueSeatIDs();

            Console.WriteLine($"Day 5a:  {CountMaxSeatID(seats)}");
            Console.WriteLine($"Day 5b:  {FindMySeat(seats)}");
        }

        private static int FindMySeat(List<int> seats)
        {
            seats.Sort();
            //Look for first missing
            int lastSeat = seats[0];
            for(int i = 1; i < seats.Count; i++)
            {
                //Was the previous seat sequential?
                if(seats[i] == (lastSeat + 1))
                {
                    //keep on keeping on
                    lastSeat = seats[i];
                }
                else
                {
                    //The previous seat was missing - that's us!
                    return lastSeat + 1;
                }
            }
            return 0;
        }

        private static int CountMaxSeatID(List<int> seats)
        {
            int max = 0;
            foreach(int id in seats)
            {
                if (id > max)
                    max = id;
            }
            return max;
        }

        public static List<int> GetAllUniqueSeatIDs()
        {
            List<int> seats = new List<int>();

            foreach (string line in File.ReadAllLines("input05.txt"))
            {
                BinaryRow row = new BinaryRow();

                foreach (char c in line)
                {
                    switch (c)
                    {
                        case 'F':
                            row.FrontHalf();
                            break;
                        case 'B':
                            row.BackHalf();
                            break;
                        case 'L':
                            row.LeftHalf();
                            break;
                        case 'R':
                            row.RightHalf();
                            break;
                    }
                }

                int rowNum = row.GetRow();
                int seatNum = row.GetSeat();
                int seatID = (rowNum * 8) + seatNum;
                seats.Add(seatID);
            }
            return seats;
        }
    }
}
