using System;
using System.Collections.Generic;
using System.Text;

namespace shapes
{
    class circle:shape
    {
        private double radius;
        public circle(double radius)
        {
            Radius=radius;
        }
        public double Radius
        {
            get { return radius; }
            set { if (!verify()) Console.WriteLine("error!");
                radius = value;
            }
        }

        public double area => Math.PI * radius * radius;
        public string info => ("radius:"+radius.ToString());
            public bool verify()
        {
            return radius > 0;
        }
    }
}
