using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _7._1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Node> list = new List<Node>();
            Node mainRoot = new Node();

            StreamReader input = new StreamReader("../../input.txt");
            while (!input.EndOfStream)
            {
                List<string> line = input.ReadLine().Split(' ').ToList();
                Node node = new Node()
                {
                    Name = line[0],
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
                } catch
                {

                }
                
                list.Add(node);
            }

            foreach(Node node in list)
            {
                bool root = false;
                bool isChild = false;
                if (!node.HasChildren())
                {
                    continue;
                }

                foreach(Node secondNode in list)
                {
                    if (!secondNode.ContainsChild(node.Name))
                    {
                        continue;
                    } else
                    {
                        isChild = true;
                        break;
                    }
                }

                if (isChild)
                {
                    continue;
                }
                else
                {
                    mainRoot = node;
                    break;
                }
            }
            Console.WriteLine("ROOT NAME: " + mainRoot.Name);
            Console.ReadLine();
        }
    }

    class Node
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public bool Leaf { get; set; }

        public List<String> Children { get; set; }

        public Node() {
            Children = new List<String>();
        }

        public bool IsLeaf()
        {
            return Leaf;
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
    }


}
