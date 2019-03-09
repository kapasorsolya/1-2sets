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

            int n, p, type;
            p = 6; // fixed (number of nodes)


            if (args.Length != 2)
            {
                System.Console.WriteLine("Required 2 numeric argument for solving exercise :)\n ");

                if (args.Length < 1)
                {
                    System.Console.WriteLine("Please enter a numeric argument for domenium");
                }
                else
                {
                    if (args.Length < 2)
                    {
                        System.Console.WriteLine("Please enter the type of solution (1-BT or 2-BT+MVR or 3 BT-AC3");
                    }
                }
                return;
            }

            n = int.Parse(args[0]);
            type = int.Parse(args[1]);

            //n = 10;
            //type = 3;

            int[] a = new int[p];
            if (type == 1)
            {

                BT(a, n, 0, p);
                System.Console.WriteLine("Bactrack: Number of assigments " + counter);
                counter = 0;

            }
            else
            {
                if (type == 2)
                {

                    List<CSP> domeniumList = new List<CSP>();
                    List<CSP> removedItemFromDomeniumList = new List<CSP>();
                    List<int> usedVariablesNumbersList = new List<int>();

                    InitializeDomeniums(n, p, domeniumList, removedItemFromDomeniumList);
                    BTWithMVR(a, n, 0, p, domeniumList, removedItemFromDomeniumList, usedVariablesNumbersList);

                    System.Console.WriteLine("BT + MVR + forward checking: Number of assigments= " + counter);
                    counter = 0;
                }
                else
                {
                    if (type == 3)
                    {
                        List<CSP> domeniumList = new List<CSP>();
                        List<CSP> removedItemFromDomeniumList = new List<CSP>();
                        List<int> usedVariablesNumbersList = new List<int>();

                        InitializeDomeniums(n, p, domeniumList, removedItemFromDomeniumList);

                        BTWithMVRAndArc(a, n, 0, p, domeniumList, usedVariablesNumbersList);

                        System.Console.WriteLine("BT + MVR + AC3: Number of assigments= " + counter);
                        counter = 0;
                    }
                    else
                    {
                        System.Console.WriteLine("Sorry we didn't recognized the type of excercise( it should be 1 or 2 or 3)");

                    }

                }
            }
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

            BuildNeighboursList(domeniumList, p);
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
            int min = Int32.MaxValue;
            int variable = -1;
            for (int i = 0; i < p; i++)
            {
                if (usedVariablesNumbersList.Contains(i) == false)
                {
                    if (min > domeniumList[i].ElementsOfDomenium.Count)
                    {
                        min = domeniumList[i].ElementsOfDomenium.Count;
                        variable = i;
                    }
                }
            }
            return variable;

        }

        private static void BTWithMVR(int[] x, int n, int k, int p, List<CSP> domeniumList, List<CSP> removedItemFromDomeniumList, List<int> usedVariablesNumbersList)
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
                            BTWithMVR(x, n, k + 1, p, domeniumList, removedItemFromDomeniumList, usedVariablesNumbersList);
                            BuildDomeniumBack(domeniumList, k, p, removedItemFromDomeniumList);

                        }
                    }
                }
            }
        }

        private static void BTWithMVRAndArc(int[] x, int n, int k, int p, List<CSP> domeniumList, List<int> usedVariablesNumbersList)
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
                        usedVariablesNumbersList.Add(min);
                        if (IsSolution(x, k, p))
                        {
                            Print(x, p);
                        }
                        else
                        {
                            Ac3(domeniumList, p);
                            BTWithMVRAndArc(x, n, k + 1, p, domeniumList, usedVariablesNumbersList);
                        }
                    }
                }
            }
        }

        private static void BuildDomeniumBack(List<CSP> domeniumList, int k, int p, List<CSP> removedItemFromDomeniumList)
        {
            List<int> list = removedItemFromDomeniumList[k].ElementsOfDomenium;
            domeniumList[k].ElementsOfDomenium.AddRange(list);
            domeniumList[k].ElementsOfDomenium.OrderBy(n2 => n2);
            removedItemFromDomeniumList[k].ElementsOfDomenium.Clear();

        }

        private static void ForwardCheckingRemoveItemFromDomenium(List<CSP> domeniumList, int value, int currentVariable, int p, List<CSP> removedItemFromDomeniumList)
        {
            for (int i = 0; i < p; i++)
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

        public static void Ac3(List<CSP> domeniumList, int p)
        {
            Queue<Arc> queue = new Queue<Arc>();

            //add items to the queue
            for (int i = 0; i < p; i++)
            {
                foreach (var item in domeniumList[i].Neighbours)
                {
                    queue.Enqueue(new Arc
                    {
                        FirstArc = i,
                        SecondArc = item

                    });
                }
            }

            int xi, xj;
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                xi = item.FirstArc;
                xj = item.SecondArc;

                if (RemoveInconsistentValues(xi, xj, domeniumList) == true)
                {
                    if (domeniumList[xi].ElementsOfDomenium.Count == 0 || domeniumList[xj].ElementsOfDomenium.Count == 0)
                    {
                        break;
                    }
                    foreach (var element in domeniumList[xi].Neighbours)
                    {
                        queue.Enqueue(new Arc
                        {
                            FirstArc = xi,
                            SecondArc = element
                        });
                    }
                }
            }

        }
        public static bool RemoveInconsistentValues(int x0, int x1, List<CSP> domeniumList)
        {
            bool removed = false;
            bool yes;
            foreach (var i in domeniumList.ElementAt(x0).ElementsOfDomenium)
            {
                yes = false;
                foreach (var j in domeniumList.ElementAt(x1).ElementsOfDomenium)
                {
                    if (i != j)
                    {
                        yes = true;
                        break;
                    }

                }
                if (!yes)
                {
                    domeniumList.ElementAt(x0).ElementsOfDomenium.Remove(i);
                    removed = true;

                }

            }
            return removed;

        }

    }

}