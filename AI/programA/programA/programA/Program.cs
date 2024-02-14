using System;
using System.Collections.Generic;

namespace Program
{
    internal class Program
    {
        static int[] goalState = new int[9] { 0, 1, 2, 4, 4, 5, 5, 5, 8 };

        class Node
        {
             List<Node> childrenNodes = new List<Node>();
             Node parent;
             
             int[] myPuzzel = new int[9];
             int index = 0,h=0;
             string action = "";
            public int getH() { return h; }
            public Node getParent() { return parent; }
            public List<Node> GetChildrenNodes() { return childrenNodes; }
            public Node(int[] p)
            {
                setPuzzel(p);
                numberOfMisplacedTiles();
            }
            public void setPuzzel(int[] puzzel)
            {
                for (int i = 0; i < puzzel.Length; i++)
                {
                    this.myPuzzel[i] = puzzel[i];
                }
            }
            public bool test()
            {
                bool goal = true;
                if (h != 0)
                    goal = false;
                return goal;
            }
            public void coppy(int[] x, int[] y)
            {
                for (int i = 0; i < y.Length; i++)
                {
                    x[i] = y[i];
                }
            }
            public void moveRight(int[] p, int i)
            {

                if (i != 2 && i != 5 && i != 8)
                {
                    int[] pc = new int[9];
                    coppy(pc, p);
                    int temp = pc[i + 1];
                    pc[i + 1] = pc[i];
                    pc[i] = temp;
                    Node chid = new Node(pc);
                    childrenNodes.Add(chid);
                    chid.parent = this;
                    chid.action = "moveRight";

                }
            }
            public void moveLeft(int[] p, int i)
            {

                if (i != 0 && i != 3 && i != 6)
                {
                    int[] pc = new int[9];
                    coppy(pc, p);
                    int temp = pc[i - 1];
                    pc[i - 1] = pc[i];
                    pc[i] = temp;
                    Node chid = new Node(pc);
                    childrenNodes.Add(chid);
                    chid.parent = this;
                    chid.action = "LEFT";
                }
            }
            public void moveUp(int[] p, int i)
            {

                if (i != 0 && i != 1 && i != 2)
                {
                    int[] pc = new int[9];
                    coppy(pc, p);
                    int temp = pc[i - 3];
                    pc[i - 3] = pc[i];
                    pc[i] = temp;
                    Node chid = new Node(pc);
                    childrenNodes.Add(chid);
                    chid.parent = this;
                    chid.action = "UP";

                }
            }
            public void moveDown(int[] p, int i)
            {

                if (i != 6 && i != 7 && i != 8)
                {
                    int[] pc = new int[9];
                    coppy(pc, p);
                    int temp = pc[i + 3];
                    pc[i + 3] = pc[i];
                    pc[i] = temp;
                    Node chid = new Node(pc);
                    childrenNodes.Add(chid);
                    chid.parent = this;
                    chid.action = "DOWN";

                }
            }
            public void printAction()
            {
                if (action != "")//inital node 
                    Console.Write(action + " ");

            }
            public void printPuzzel()
            {

                Console.WriteLine();
                int x = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (myPuzzel[x] == 0)
                            Console.Write("  ");
                        else
                            Console.Write(myPuzzel[x] + " ");
                        x++;
                    }
                    Console.WriteLine();

                }
                Console.WriteLine();

            }
            public void expand()
            {
                for (int i = 0; i < myPuzzel.Length; i++)
                {
                    if (myPuzzel[i] == 0)
                    {
                        index = i;
                        moveRight(myPuzzel, index);
                        moveLeft(myPuzzel, index);
                        moveUp(myPuzzel, index);
                        moveDown(myPuzzel, index);
                        i=myPuzzel.Length;
                    }
                }
            }
            public void numberOfMisplacedTiles()
            {
                for (int i = 0; i < myPuzzel.Length; i++)
                {
                    if (myPuzzel[i] != goalState[i])
                    {
                        h++;//Calulating F(n)
                    }
                }
            }
        }
        class greedyClass
        {
            List<Node> pathToGool = new List<Node>();
            List<Node> expanded = new List<Node>();
            List<Node> generated = new List<Node>();

            public List<Node> GeedySearch(Node initial)
            {
                generated.Add(initial);
                if (initial.getH() == 0) { return pathToGool; }
                expanded.Add(initial);
                initial.expand();
                int min = 10;
                bool foundGool = false;

                Node myNode = initial;
                Node currentChild = null;

                while (!foundGool)
                {
                    foreach (Node child in myNode.GetChildrenNodes())
                    {
                        generated.Add(child);
                        if (min > child.getH())
                        {
                            min = child.getH();
                            currentChild = child;
                        }
                    }

                    if (currentChild != null)
                    {
                        if (currentChild.test())
                        {
                            Console.WriteLine();
                            Console.WriteLine();

                            foundGool = true;
                            pathTrace(pathToGool, currentChild);
                            break;
                        }
                        else
                        {

                            myNode = currentChild;
                            myNode.expand();
                            expanded.Add(myNode);

                            min = 10;
                        }
                    }
                }

                return pathToGool;
            }

            public void pathTrace(List<Node> path, Node n)
            {

                Node current = n;
                path.Add(current);
                while (current != null)
                {
                    current = current.getParent();
                    path.Add(current);
                }

            }
            public void getNumbers()
            {
                Console.WriteLine("Number of generated nodes = {0} ", generated.Count);
                Console.WriteLine("Number of expanded nodes = {0}\n", expanded.Count);
            }
        }
        
        static void Main(string[] args)
        {
            int[] puzzel = new int[9];
            string input = "";
            Console.WriteLine("Enter the starting position as a string (i.e. 1424555S8   the S is used for space)");
            input = Console.ReadLine();
            Console.WriteLine("\n");
             //converting to int 
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'S')
                    puzzel[i] = 0;
                else
                    puzzel[i] = int.Parse(input[i].ToString());
            }
            int x = 0;
            Console.WriteLine("Printing your current myPuzzel...");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzel[x] == 0)
                        Console.Write("  ");
                    else
                        Console.Write(puzzel[x] + " ");
                    x++;
                }
                Console.WriteLine();

            }
            

                Node initNode = new Node(puzzel);
                greedyClass mySearch = new greedyClass();
                List<Node> solution = mySearch.GeedySearch(initNode);

                if (solution.Count > 0)
                {

                    solution.Reverse();


                    Console.WriteLine("Found your Goal!: ");
                    solution[solution.Count - 1].printPuzzel();
                    Console.WriteLine("\nTracing path to your goal ...");
                    Console.Write("Actions : ");
                    for (int i = 0; i < solution.Count; i++)
                    {
                        if (solution[i] != null)
                            solution[i].printAction();
                        if (i < solution.Count - 1 && i > 1)
                            Console.Write(" --> ");


                    }

                    Console.WriteLine("\n");

                    mySearch.getNumbers();

                }
            else
            {
                Console.WriteLine("No action needed your are in a goal state\n");
                mySearch.getNumbers();
            }
        }
           

    }
}
