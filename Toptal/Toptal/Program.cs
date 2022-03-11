using System;
using System.Collections.Generic;
using System.Linq;

namespace Toptal
{
    internal class Program
    {
        static void Main(string[] args)
        {


            int[] A = new int[]
            {
7,5,2,7,2,7,4,7

            };



            //var result = solution(A);

            Console.WriteLine(result);

        }
        private static int Exercise2(int[] integerArray)
        {
            Array.Sort<int>(integerArray);

            int i = 0;
            while (i < integerArray.Length && integerArray[i] < 0)
            {
                i++;
            }
            if (i == integerArray.Length)
            {
                return 1;
            }

            var missingvaluecandidate = integerArray[i] + 1;
            while (i < integerArray.Length)
            {

                if (missingvaluecandidate < integerArray[i + 1])
                {
                    return missingvaluecandidate;
                }
                i++;
                missingvaluecandidate = integerArray[i] + 1;
            }
            return missingvaluecandidate;


        }
        private static int Exercise1(int[] integerArray)
        {

            //remove negatives
            integerArray = integerArray.Where(x => x > 0).ToArray();

            //order
            Array.Sort<int>(integerArray);

            //1 1 2 3 4 6
            if (integerArray[0] == 0)
            {
                return 1;
            }

            int i = 1;

            while (true)
            {
                var found = Array.IndexOf<int>(integerArray, i, i - 1);
                if (found == -1)
                {
                    return i;
                }

                i++;
            }


        }

        public static int solution(int[] X, int S)
        {
            Dictionary<int, int> prefixes = new Dictionary<int, int>();
            int result = 0;
            int[] P = new int[X.Length + 1];
            prefixes.Add(0, 1);

            int[] Q = new int[X.Length + 1];
            P[0] = 0;
            Q[0] = 0;

            for (int i = 1; i < X.Length + 1; i++)
            {
                P[i] = P[i - 1] + X[i - 1];
                Q[i] = P[i] - S * i;

                if (!prefixes.ContainsKey(Q[i]))
                {
                    prefixes.Add(Q[i], 1);
                }
                else
                {
                    int temp = prefixes[Q[i]];
                    temp++;
                    prefixes.Add(Q[i], temp);

                }

            }


            foreach (var entry in prefixes)
            {
                int value = entry.Value;
                result += value * (value - 1) / 2;
            }

          
            return result;
        }

        private static string Exercise3()
        {
            return "Hello World 3";
        }

        private static string Exercise4()
        {
            return "Hello World 3";
        }


    }



    class Test
    {
        public Test(String number, string group, bool passed)
        {
            this.number = number;
            this.group = group;
            this.passed = passed;

        }

        public String number { get; set; }
        public String group { get; set; }

        public bool passed { get; set; }

    }

}
