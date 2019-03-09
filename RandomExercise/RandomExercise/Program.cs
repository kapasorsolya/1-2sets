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
            //if (args.Length == 1)
            //{
            //    System.Console.WriteLine("Please enter a numeric argument.");
            //    System.Console.WriteLine("Usage: Factorial <num>");
            //    return;
            //}

            int n = 9;
            int p = 6;
            int[] a = new int[p];

            BT(a, n, 0, p);

            List<CSP> domeniumList = new List<CSP>();
            List<CSP> removedItemFromDomeniumList = new List<CSP>();
            List<int> usedVariablesNumbersList = new List<int>();

            Console.WriteLine("Bactrack: Number of assigments " + counter);

            InitializeDomeniums(n,p, domeniumList, removedItemFromDomeniumList);

            // print neighboursList
            //for(int i=0;i<p;i++)
            //{
            //    Console.Write(i + "->");
            //    foreach (var item in domeniumList[i].Neighbours)
            //    {
            //        Console.Write(item + " ");
            //    }
            //    Console.WriteLine();
            //}
            
            //BTWithMVR(a, n, 0, p,domeniumList,removedItemFromDomeniumList,usedVariablesNumbersList);
            int value = MinimumRemainValues(domeniumList, usedVariablesNumbersList, p);
            Console.WriteLine("BT + MVR + forward checking: Number of assigments= " + counter);
            Console.WriteLine("Press Key....");
            Console.ReadKey();
            
        }

        private static void InitializeDomeniums(int n, int p, List<CSP> domeniumList, List<CSP> removedItemFromDomeniumList)
        {
            List<int> itemsOfDomenium;
            itemsOfDomenium = Enumerable.Range(1, n).ToList();
            List<int> evenNumbers = itemsOfDomenium.Where(n1 => n1 % 2 == 0).ToList();

            for (int i = 0; i < p; i++)
            { 

                 removedItemFromDomeniumList.Add(new CSP
                {
                    ElementsOfDomenium = new List<int>(),
                     
                });

                if (i == p - 2)
                {

                    domeniumList.Add(new CSP
                    {
                        ElementsOfDomenium = evenNumbers,
                        Neighbours = new List<int>() 
                    });

                }
                else
                {
                    domeniumList.Add(new CSP
                    {
                        ElementsOfDomenium = itemsOfDomenium,
                        Neighbours = new List<int>()
                    });
                }
            }

            BuildNeighboursList(domeniumList,p);
        }

        private static void BuildNeighboursList(List<CSP> domeniumList, int p)
        {
            List<int> neighbours = new List<int>();
            for (int i = 0; i < p; i++)
            {
                switch (i)
                {
                    case 0:
                        neighbours.Add(1);
                        domeniumList[i].Neighbours.AddRange(neighbours);
                        neighbours.Clear();
                        break;
                    case 1:
                        neighbours.Add(0);
                        neighbours.Add(3);
                        domeniumList[i].Neighbours.AddRange(neighbours);
                        neighbours.Clear();
                        break;
                    case 2:
                        neighbours.Add(1);
                        neighbours.Add(3);
                        neighbours.Add(4);
                        domeniumList[i].Neighbours.AddRange(neighbours);
                        neighbours.Clear();
                        break;
                    case 3:
                        neighbours.Add(2);
                        domeniumList[i].Neighbours.AddRange(neighbours);
                        neighbours.Clear();
                        break;
                    case 4:
                        neighbours.Add(2);
                        neighbours.Add(5);
                        domeniumList[i].Neighbours.AddRange(neighbours);
                        neighbours.Clear();
                        break;
                    case 5:
                        neighbours.Add(4);
                        domeniumList[i].Neighbours.AddRange(neighbours);
                        neighbours.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        private static int MinimumRemainValues(List<CSP> domeniumList, List<int> usedVariablesNumbersList, int p)
        {
            int min =Int32.MaxValue;
            int variable = -1;
            for(int i=0;i<p; i++)
            {
                if(usedVariablesNumbersList.Contains(i) == false)
                {
                    if(min > domeniumList[i].ElementsOfDomenium.Count)
                    {
                        min = domeniumList[i].ElementsOfDomenium.Count;
                        variable = i;
                    }
                }
            }
            return variable;
            
        }

        private static void BTWithMVR(int[] x, int n, int k, int p, List<CSP> domeniumList, List<CSP> removedItemFromDomeniumList,List<int> usedVariablesNumbersList)
        {
        
            int min = MinimumRemainValues(domeniumList, usedVariablesNumbersList, p);
            List<int> list = domeniumList.ElementAt(k).ElementsOfDomenium;
          
            for (int i = 0; i < list.Count; i++)
            {
                x[k] = list[i];
                counter++;
                if (firstSolution == false)
                {
                    if (IsPromising(x, p, k))
                    {
                        ForwardCheckingRemoveItemFromDomenium(domeniumList, x[k], k, p, removedItemFromDomeniumList);
                        usedVariablesNumbersList.Add(min);
                        if (IsSolution(x, k, p))
                        {

                            Print(x, p);
                        }
                        else
                        {
                           
                            BTWithMVR(x, n, k + 1, p, domeniumList, removedItemFromDomeniumList,usedVariablesNumbersList);
                            BuildDomeniumBack(domeniumList, k, p, removedItemFromDomeniumList);

                        }
                    }
                }
            }
        }

        private static void BuildDomeniumBack(List<CSP> domeniumList, int k, int p, List<CSP> removedItemFromDomeniumList)
        {
            List<int> list = removedItemFromDomeniumList[k].ElementsOfDomenium;
            domeniumList[k].ElementsOfDomenium.AddRange(list);
            domeniumList[k].ElementsOfDomenium.OrderBy(n2 =>n2);
            removedItemFromDomeniumList[k].ElementsOfDomenium.Clear();

        }

        private static void ForwardCheckingRemoveItemFromDomenium(List<CSP> domeniumList, int value, int currentVariable, int p, List<CSP> removedItemFromDomeniumList)
        {
            for(int i=0;i<p;i++)
            {
                if (domeniumList[i].Neighbours.Contains(currentVariable) && domeniumList[currentVariable].Neighbours.Contains(i))
                {
                    domeniumList[i].ElementsOfDomenium.RemoveAll(n => n == value);
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
        public static void Ac3(List<CSP> domeniumList,int[,]matrix)
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
        public static bool RemoveInconsistentValues(int x0, int x1, List<CSP> domeniumList){
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