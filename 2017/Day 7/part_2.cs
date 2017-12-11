using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2
{
    class Program
    {
        static List<Node> list;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            list = new List<Node>();
            Node mainRoot = new Node();

            StreamReader input = new StreamReader("../../input.txt");
            while (!input.EndOfStream)
            {
                List<string> line = input.ReadLine().Split(' ').ToList();
                Node node = new Node()
                {
                    Name = line[0],
                    Weight = Int32.Parse(line[1].Replace("(", "").Replace(")", ""))
                };
                line.Remove(node.Name);
                line.RemoveAt(0);
                try
                {
                    line.RemoveAt(0);
                    foreach (string name in line)
                    {
                        node.Children.Add(name.Replace(",", ""));
                    }
                }
                catch
                {

                }

                list.Add(node);
            }

            list.First(n=> n.Name == "dtacyn").AddChildren(list);
            getUnbalancedRoot("dtacyn");
            string userInput;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("NEXT?:");
                userInput = Console.ReadLine();
                if (userInput == "exit") exit = true; else getUnbalancedRoot(userInput); 

            }


            /*
            foreach(Node node in list)
            {
                if (!node.HasChildren())
                {
                    continue;
                }

                List<int> weights = new List<int>();
                foreach (string name in node.Children)
                {
                    Node foundMe = list.First(n => n.Name == name);
                    weights.Add(foundMe.Weight);
                }

                if (list.Any(o => o != list[0]))
                {
                    foreach(int weight in weights)
                    {
                        Console.Write(weight + " ");
                        Console.WriteLine();
                    }
                }
            }
            */
            Console.Write("Finished");
            Console.ReadLine();
        }

        private static void getUnbalancedRoot(string name)
        {
            Node found = list.First(n => n.Name == name);
            Dictionary<string, int> children = new Dictionary<string, int>();
            foreach (string newName in found.Children)
            {
                Node foundMe = list.First(n => n.Name == newName);
                children.Add(foundMe.Name, foundMe.GetTotalWeight());
            }

            foreach(KeyValuePair<string,int> pair in children)
            {
                Console.WriteLine("Node: " + pair.Key + ". Weight: " + pair.Value);
            }

            /*
            if (children.Any(o => o != children[0]))
            {
                foreach (int weight in weights)
                {
                    Console.Write(weight + " ");
                    Console.WriteLine();
                }
            }
            */
        }

        private static int getChildrenWeights(Node node)
        {
            List<Node> children = new List<Node>();
            int weights = node.Weight;
            foreach (string name in node.Children)
            {
                Node foundMe = list.First(n => n.Name == name);
                if (foundMe.HasChildren())
                {
                    weights += getChildrenWeights(foundMe);
                } else
                {
                    //return foundMe.Weight;
                }
            }
            return weights;
        }


    }

    class Node
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public bool Leaf { get; set; }
        public int Weight { get; set; }
        public bool Balanced { get; set; }

        public List<String> Children { get; set; }

        public List<Node> ChildNodes { get; private set; }

        public Node()
        {
            Children = new List<String>();
        }

        public bool IsLeaf()
        {
            return Leaf;
        }

        public void AddChildren(List<Node> nodes)
        {
            ChildNodes = new List<Node>();
            foreach(string child in Children)
            {
                ChildNodes.Add(nodes.First(n => n.Name == child));
                ChildNodes[ChildNodes.Count - 1].AddChildren(nodes);
            }
        }

        public void AddChild(string name)
        {
            Children.Add(name);
        }

        public bool HasChildren()
        {
            return Children.Count() > 0;
        }

        public bool ContainsChild(string name)
        {
            return Children.Contains(name);
        }

        public int GetTotalWeight()
        {
            return Weight + ChildNodes.Sum(c => c.GetTotalWeight());
        }

        public bool AreChildrenEqual()
        {
            return ChildNodes.Select(n => n.GetTotalWeight()).Distinct().Count() == 1;
        }
    }
}