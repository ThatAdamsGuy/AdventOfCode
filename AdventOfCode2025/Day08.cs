using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2025
{
    internal class Day08
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day08input.txt");
            List<Point> points = new List<Point>();
            List<Connection> possibleConnections = new List<Connection> ();
            List<HashSet<Point>> networks = new List<HashSet<Point>>();
            foreach(var line in input)
            {
                var split = line.Split(',');
                points.Add(new Point(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
            }
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    possibleConnections.Add(new Connection(points[i], points[j]));
                }
            }
            possibleConnections = possibleConnections.OrderBy(x => x.Distance).ToList();
            int counter = 0;
            while (true)
            {
                var shortestConnection = possibleConnections.First();
                possibleConnections.Remove(shortestConnection);
                if(networks.Any(x => x.Contains(shortestConnection.Point1) || x.Contains(shortestConnection.Point2)))
                {
                    // A network already exists with one of these points, add the other.
                    var possibleNetworks = networks.Where(x => x.Contains(shortestConnection.Point1) || x.Contains(shortestConnection.Point2)).ToList();
                    if (possibleNetworks.Count == 1)
                    {
                        var network = possibleNetworks.Single();
                        network.Add(shortestConnection.Point1);
                        network.Add(shortestConnection.Point2);
                    }
                    else if (possibleNetworks.Count == 2)
                    {
                        // Join the two networks
                        var newNetwork = new HashSet<Point>(possibleNetworks[0]);
                        newNetwork.UnionWith(possibleNetworks[1]);
                        newNetwork.Add(shortestConnection.Point1);
                        newNetwork.Add(shortestConnection.Point2);

                        // Remove the old networks and add the merged one
                        networks.Remove(possibleNetworks[0]);
                        networks.Remove(possibleNetworks[1]);
                        networks.Add(newNetwork);
                    }
                    else
                    {
                        throw new Exception("Point in 3 networks");
                    }
                }
                else
                {
                    networks.Add(new HashSet<Point>() { shortestConnection.Point1,  shortestConnection.Point2 });
                }
                counter++;
                if(counter == 1000)
                {
                    var topThree = networks.OrderByDescending(x => x.Count()).Take(3).Aggregate(1, (product, network) => product * network.Count);
                    Console.WriteLine("Part 1 - " + topThree);
                }
                if(networks.Count == 1 && networks.Single().Count == 1000)
                {
                    Console.WriteLine("Part 2 - " + shortestConnection.Point1.X * shortestConnection.Point2.X);
                    break;
                }
            }
        }

        class Connection
        {
            public Point Point1 { get; set; }
            public Point Point2 { get; set; }
            public double Distance { get; set; }
            public Connection(Point point1, Point point2)
            {
                Point1 = point1;
                Point2 = point2;
                Distance = GetDistance(point1, point2);
            }

            private double GetDistance(Point point1, Point point2)
            {
                return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2) + Math.Pow(point1.Z - point2.Z, 2));
            }
        }

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public Point Connection { get; set; }
            public Point(int x, int y, int z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
        }
    }
}
