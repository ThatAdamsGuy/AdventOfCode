using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    class Day09
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day09input.txt");
            long maxArea = int.MinValue;
            long maxX1 = int.MinValue;
            long maxY1 = int.MinValue;
            long maxX2 = int.MinValue;
            long maxY2 = int.MinValue;

            for (int i = 0; i < input.Length; i++)
            {
                for (int k = i + 1; k < input.Length; k++)
                {
                    var point1 = input[i].Split(',');
                    var point2 = input[k].Split(',');

                    long x1 = long.Parse(point1[0]);
                    long x2 = long.Parse(point2[0]);
                    long y1 = long.Parse(point1[1]);
                    long y2 = long.Parse(point2[1]);

                    long xLen = Math.Abs(x1 - x2) + 1;
                    long yLen = Math.Abs(y1 - y2) + 1;

                    var area = xLen * yLen;
                    if (maxArea < area)
                    {
                        maxArea = area;
                        maxX1 = x1;
                        maxY1 = y1;
                        maxX2 = x2;
                        maxY2 = y2;
                    }
                }
            }

            Console.WriteLine($"Part 1 - {maxArea} (({maxX1},{maxY1}) to ({maxX2},{maxY2}))");

            var vertices = input
                .Select(line => line.Split(','))
                .Where(parts => parts.Length >= 2)
                .Select(parts => new Point((int)long.Parse(parts[0]), (int)long.Parse(parts[1])))
                .ToArray();

            maxArea = 0;

            var lines = new Line[vertices.Length];
            for (var i = 0; i < vertices.Length - 1; i++)
            {
                lines[i] = new Line(vertices[i], vertices[i + 1]);
            }
            lines[vertices.Length - 1] = new Line(vertices[vertices.Length - 1], vertices[0]);

            for (var i = 0; i < vertices.Length - 1; i++)
            {
                var pointA = vertices[i];
                for (var j = i + 1; j < vertices.Length; j++)
                {
                    var pointB = vertices[j];
                    var area = RectangleArea(pointA, pointB);

                    if (area <= maxArea)
                    {
                        continue;
                    }

                    if (!lines.Any(line => line.Intersects(pointA, pointB)))
                    {
                        maxArea = area;
                    }
                }
            }

            Console.WriteLine($"Part 2 - {maxArea}");
        }

        private static long RectangleArea(Point pointA, Point pointB)
        {
            long width = Math.Abs(pointB.X - pointA.X) + 1;
            long height = Math.Abs(pointB.Y - pointA.Y) + 1;
            return width * height;
        }

        class Line
        {
            public Point Start { get; set; }
            public Point End { get; set; }

            public Line(Point start, Point end)
            {
                this.Start = start;
                this.End = end;
            }

            public bool Intersects(Point pointA, Point pointB)
            {
                var minX = Math.Min(pointA.X, pointB.X);
                var maxX = Math.Max(pointA.X, pointB.X);
                var minY = Math.Min(pointA.Y, pointB.Y);
                var maxY = Math.Max(pointA.Y, pointB.Y);

                var segMinX = Math.Min(Start.X, End.X);
                var segMaxX = Math.Max(Start.X, End.X);
                var segMinY = Math.Min(Start.Y, End.Y);
                var segMaxY = Math.Max(Start.Y, End.Y);

                return segMaxX > minX && segMinX < maxX && segMaxY > minY && segMinY < maxY;
            }
        }
    }
}
