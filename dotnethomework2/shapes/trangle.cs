using System;
using System.Collections.Generic;
using System.Text;

namespace shapes
{
    class trangle:shape
    {
        private double[] edge = new double[3];
        public trangle(double a ,double b, double c)
        {
            double[] Edge = new double[3] { a, b, c };
            this.edge = Edge;
        }
        public string info { get => $"traingle a={edge[0]} b={edge[1]} c={edge[2]}"; }
        public double area { get
            {
                double p = (edge[0] + edge[1] + edge[2]) / 2;
                return Math.Sqrt(p * (p - edge[0]) * (p - edge[1]) * (p - edge[2]));
            }
        }
        public double[] Edges {
            get { return edge; }
            set { if (!verify()) Console.WriteLine("ERROR!"); }
        }

        public bool verify()
        {
            double a = edge[0], b = edge[1], c = edge[2];
            return (a > 0 && b > 0 && c > 0 && a + b > c && b + c > a && c + a > b);//三角形成立的条件
        }
    }
}
