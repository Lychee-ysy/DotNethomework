using System;
using System.Collections.Generic;
using System.Text;

namespace shapes
{
    class ShapeFactory
    {
        public static Random randoms = new Random();
        public static shape creatShape(string type,params double[] edges)
        {
        
                switch (type)
                {
                    case "circle":return new circle(edges[0]);
                    case "trangle":return new trangle(edges[0],edges[1],edges[2]);
                default:throw new InvalidOperationException(type);
                }
        }
        public static shape creatRandomshape()
        {
            int type = randoms.Next(0,2);
            shape result = null;
            while(result==null)
            {
                try
                {
                    switch (type)
                    {
                        case 0:result = creatShape("circle", randoms.Next(200));break;
                        case 1:result = creatShape("trangle", randoms.Next(100));break;
                    }

                }
                catch
                {
                    Console.WriteLine("ERROR!");
                }
            }
            return result;
        }
    }
}
