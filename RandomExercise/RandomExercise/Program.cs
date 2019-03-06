using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomExercise
{
    class Program
    {
        static int counter = 0;
        static bool firstSolution = false;

        static void Main(string[] args)
        {
            int n = 9;
            int p = 6;
            int[] a = new int[p];

            BT(a, n, 0, p);
           
            Console.WriteLine("Bactrack: Number of assigments " + counter);

            Console.WriteLine("Press Key....");
            Console.ReadKey();
            
        }


        private static void BT(int[] x, int n, int k, int p)
        {
            for (x[k] = 1; x[k] <= n; x[k]++)
            {
                if (firstSolution == false)
                {
                    if (IsPromising(x, p, k))
                    {
                        counter++;
                        if (IsSolution(x, k, p))
                        {
                            Print(x, p);

                        }
                        else
                        {
                            BT(x, n, k + 1, p);
                        }
                    }
                }
            }
        }



        private static bool IsSolution(int[] x, int k, int p)
        {
            if (k < p - 1)
            {
                return false;
            }
            else
            {
                firstSolution = true;
                return true;
            }
        }

        private static void Print(int[] x, int n)
        {
            int i;
            for (i = 0; i < n; i++)
            {
                Console.Write(x[i] + " ");
            }
            Console.WriteLine();
        }

        private static bool IsPromising(int[] x, int p, int k)
        {
            if (k == p - 1)
            {
                if (x[k] != x[k - 1] / 2)
                {
                    return false;
                }
            }

            int i;
            for (i = 0; i <= k - 1; i++)
            {
                if (x[i] == x[k])
                {
                    return false;
                }
            }
            return true;
        }

    }

}