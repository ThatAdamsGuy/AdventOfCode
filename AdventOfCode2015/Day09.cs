using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    //DJIKSTRA LETS GOOOOOO
    static class Day09
    {
        [Serializable]
        public class Node
        {
            public string Name { get; set; }
            public Dictionary<Node, int> Connections { get; set; }

            public Node(string name)
            {
                Name = name;
                Connections = new Dictionary<Node, int>();
            }
        }

        public static void Run()
        {
            var graph = CreateGraph();

            foreach(var node in graph)
            {
                // Start with fresh graph
                var copy = DeepClone(graph);

                // Get the appropriate starting node
                var startingNode = copy.Single(x => x.Key.Name == node.Key.Name);
            }
        }

        private static Dictionary<Node, bool> CreateGraph()
        {
            var graph = new Dictionary<Node, bool>();
            foreach (var line in File.ReadAllLines("Day09Input.txt"))
            {
                var split = line.Split(' ');
                Node startNode = null;
                Node endNode = null;
                if (graph.Any(x => x.Key.Name == split[0]))
                {
                    startNode = graph.Single(x => x.Key.Name == split[0]).Key;
                }
                else
                {
                    startNode = new Node(split[0]);
                    graph.Add(startNode, false);
                }
                if (graph.Any(x => x.Key.Name == split[2]))
                {
                    endNode = graph.Single(x => x.Key.Name == split[2]).Key;
                }
                else
                {
                    endNode = new Node(split[2]);
                    graph.Add(endNode, false);
                }

                if (!startNode.Connections.ContainsKey(endNode))
                {
                    startNode.Connections.Add(endNode, int.Parse(split[4]));
                }
                if (!endNode.Connections.ContainsKey(startNode))
                {
                    endNode.Connections.Add(startNode, int.Parse(split[4]));
                }
            }
            return graph;
        }

        // Source - https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
