using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

           //BT(a, n, 0, p);
           
            Console.WriteLine("Bactrack: Number of assigments " + counter);

            List<Domenium> domeniumList = new List<Domenium>();
            List<int> itemsOfDomenium;
            itemsOfDomenium = Enumerable.Range(1, n).ToList();

            List<int> evenitemsInDomenium;
            itemsOfDomenium = Enumerable.Range(1, n).ToList();

            List<Domenium> removedItemFromDomeniumList = new List<Domenium>();

            List<int> evenNumbers = itemsOfDomenium.Where(n1 => n1 % 2 == 0).ToList();
            foreach(var item in evenNumbers)
            {
                Console.WriteLine(item + " ");
            }
            for (int i = 0; i < p; i++)
            {
                removedItemFromDomeniumList.Add(new Domenium
                {
                    ElementsOfDomenium = new List<int>()
                });

                if (i == p - 2 || i==p-1)
                {
                   
                    domeniumList.Add(new Domenium
                    {
                        ElementsOfDomenium = evenNumbers
                    });

                }
                else
                {
                    domeniumList.Add(new Domenium
                    {
                        ElementsOfDomenium = itemsOfDomenium
                    });
                }

              
               
            }

            int[,] matrix = new int[p, p];

            BuildNeighbours(p,matrix);
            MVR(a, n, 0, p,domeniumList,removedItemFromDomeniumList,matrix);
            Console.WriteLine("MVR: Number of assigments " + counter);
            Console.WriteLine("Press Key....");
            Console.ReadKey();
            
        }

        private static void BuildNeighbours(int p,int[,] matrix)
        {
            
            //build neighbours;
            matrix[0, 1] = 1;
            matrix[1, 0] = 1;
            matrix[1, 2] = 1;
            matrix[2, 1] = 1;
            matrix[2, 3] = 1;
            matrix[3, 2] = 1;
            matrix[2, 4] = 1;
            matrix[4, 2] = 1;
            matrix[4, 5] = 1;
            matrix[5, 4] = 1;

            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        matrix[i, j] = i * 3 + j;
            //        Console.Write(matrix[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

          


        }

        private static void MVR(int[] x, int n, int k, int p, List<Domenium> domeniumList, List<Domenium> removedItemFromDomeniumList, int[,] neighbours)
        {
            List<int> list = domeniumList.ElementAt(k).ElementsOfDomenium;
    
            
            for (int  i=0;i<list.Count;i++)
            {
                x[k] = list[i];
                counter++;
                if (firstSolution == false)
                {
                    if (IsPromising(x, p, k))
                    {
                        RemoveItemFromDomenium(domeniumList, x[k], k, p, removedItemFromDomeniumList,neighbours);

                        if (IsSolution(x, k, p))
                        {
                            
                            Print(x, p);
                        }
                        else
                        {
                            //BuildDomeniumBack(domeniumList, n, p);
                            BT(x, n, k + 1, p);

                        }
                    }
                

            }
        }
        }
        

        //private static void BuildDomeniumBack(List<Domenium> domeniumList, int n, int p)
        //{
        //   for(int i=0;i<)
        //}

        private static void RemoveItemFromDomenium(List<Domenium> domeniumList, int value, int currentVariable, int p, List<Domenium> removedItemFromDomeniumList,int[,] matrix)
        {
            for(int i=0;i<p;i++)
            {
                if(matrix[currentVariable,i] ==1 || matrix[i,currentVariable] == 1)
                {
                    var count = domeniumList[i].ElementsOfDomenium.Where(n => n == value).Count();
                    domeniumList[i].ElementsOfDomenium.RemoveAll(n => n == value);
                    //domeniumList.Capacity = domeniumList.Capacity - count;
                    removedItemFromDomeniumList[i].ElementsOfDomenium.Add(value);
                }
            }
            
               
              
            
        }

        private static void BT(int[] x, int n, int k, int p)
        {
            for (x[k] = 1; x[k] <= n; x[k]++)
            {
                counter++;
                if (firstSolution == false)
                {
                    if (IsPromising(x, p, k))
                    {
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
                //firstSolution = true;
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
        public static void Ac3(List<Domenium> domeniumList,int[,]matrix)
        {
            Queue queue=new Queue();

            while (queue.Count!=null)
            {
                //if (RemoveInconsistentValues(x0, x1, domeniumList))
                //{
                //    for (int i = 0; i < matrix.GetLength(0); i++)
                //    {
                //        queue = (matrix[x0, i], x0);
                //    }
                //}
            }
            
        }
        public static bool RemoveInconsistentValues(int x0, int x1, List<Domenium> domeniumList){
            bool removed=false;
            bool yes;
            foreach(var i in domeniumList.ElementAt(x0).ElementsOfDomenium){
                yes=false;
                foreach(var j in domeniumList.ElementAt(x1).ElementsOfDomenium){
                    if(i!=j){
                    yes=true;
                    break;
                        }

                }
                 if(!yes)
                {
                 domeniumList.ElementAt(x0).ElementsOfDomenium.Remove(i);
                    removed=true;
                    
                }

            }                   
            return removed;
        }

    }

}