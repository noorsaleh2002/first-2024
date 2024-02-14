using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Knapsack(int[,] array, int maxweight)
        {
            //----------------------------------------------------
            int n = array.GetLength(1);// number of values
            List<int> parent = new List<int>();//initially empty 
            List<int> childe = new List<int>();
            List<int> notPikedItems = new List<int>();
            List<int> ints = new List<int>();
            int currentWeight = 0, childeWeight = 0;
            int currentValue = 0, childeValue = 0;
            int maxValue = 0; 
            bool notReachMAx = true;
            Random rnd = new Random();
            Stopwatch stopwatch = new Stopwatch();
            //----------------------------------------------------

            for (int i=0; i<array.GetLength(1);i++)
                maxValue+=array[1,i];

            for (int i = 0; i < array.GetLength(1); i++) { ints.Add(i); }
            //create random parent state
            for (int i = 0; i < n; i++)
            {
                int x = rnd.Next(0, ints.Count);//random index
                if (!parent.Contains(x) && currentWeight + array[0, x] <= maxweight)
                {
                    parent.Add(x);
                    currentWeight += array[0, x];
                    currentValue += array[1, x];

                }
                ints.Remove(x);
            }
            
            stopwatch.Start();
            while (notReachMAx)
            {
            
                if (currentWeight <= maxweight && maxValue == currentValue)
                { 
                    notReachMAx = false; break; 
                }
                else
                {
                    notPikedItems = GetItems(parent, array);
                    //swap funtion ==> creating childe state forme the parent state randomly

                    childe = swap(notPikedItems, new List<int>(parent));
                    childeValue = 0; childeWeight = 0;
                    for (int i = 0; i < childe.Count; i++)
                    {
                        childeValue += array[1, childe[i]];
                        childeWeight += array[0, childe[i]];
                    }
                    if (!(childeWeight > maxweight))
                    {
                        if (childeValue > currentValue && (childeWeight<=currentWeight || childeWeight>currentWeight))

                        {
                            parent.Clear();
                            parent.AddRange(childe);
                            childe.Clear();
                            maxValue = 0;
                            currentValue = childeValue;
                            currentWeight = childeWeight;
                            if(currentWeight!=maxweight)//adding funtion
                            {    for(int i=0;i<array.GetLength(1);i++)
                                if (!parent.Contains(i) && currentWeight + array[0, i] <= maxweight)
                                {
                                    parent.Add(i);
                                    currentWeight += array[0, i];
                                    currentValue += array[1, i];
                                    

                                }
                            }
                        }
                    }
                    // Check if 10 seconds have passed
                    if (stopwatch.ElapsedMilliseconds >= 10000) //10000
                    {
                        // Break out of the loop
                        notReachMAx = false;
                        break;
                    }

                    // Optionally, add a small delay to avoid high CPU usage
                    Thread.Sleep(100);
                }
            }
            stopwatch.Stop();
            //printing the resulte
            parent.Sort();
            Console.WriteLine("output");
            foreach (int i in parent) { Console.Write(i); Console.Write(" "); }
            Console.WriteLine();
            Console.WriteLine(currentWeight);
            Console.WriteLine(currentValue);

        }
        static List<int> swap(List<int> notPikedItems, List<int> parent)
        {

            Random rnd = new Random();
            int x = rnd.Next(0, parent.Count);//random index
            int removedValue = parent[x];
            parent.Remove(removedValue);
            int y = rnd.Next(0, notPikedItems.Count);//rendom index
            int pikedValue = notPikedItems[y];
            parent.Add(pikedValue);
            notPikedItems.Remove(pikedValue);
            notPikedItems.Add(removedValue);
            return parent;
        }
        static List<int> GetItems(List<int> perent, int[,] array)
        {
            List<int> item = new List<int>();
            for (int i = 0; i < array.GetLength(1); i++)
            {

                if (!perent.Contains(i)) item.Add(i);
            }
            return item;
        }
        static void Main(string[] args)
        {
            List<int> weight = new List<int>();
            List<int> value = new List<int>();
            int totalWeight = 0;
            string weights = "", values = "";
            Console.Write("Enter wights: ");
            weights = Console.ReadLine();
            Regex re = new Regex(@"[0-9]+");
            var matches = re.Matches(weights);
            foreach (Match match in matches)
            {
                weight.Add(int.Parse(match.Value));
            }
            Console.Write("Enter values: ");
            values = Console.ReadLine();
            matches = re.Matches(values);
            foreach (Match match in matches)
            {
                value.Add(int.Parse(match.Value));
            }


            int[,] wightValue = new int[2, weight.Count];
            for (int i = 0; i < weight.Count; i++)
                wightValue[0, i] = weight[i];
            for (int i = 0; i < value.Count; i++)
                wightValue[1, i] = value[i];

            Console.Write("Enter total weight: ");
            totalWeight = int.Parse(Console.ReadLine());
            Console.WriteLine("The max time for output is 10 sec ...");

            Knapsack(wightValue, totalWeight);

        }
    }
}

