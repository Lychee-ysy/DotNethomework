using System;
using System.Collections.Generic;

namespace shapes
{
  
    class Program
    {
        static void Main(string[] args)
        {
            List<shape> shapes = new List<shape>();
            for (int i=0;i<10;i++)
            {
                shapes.Add(ShapeFactory.creatRandomshape());
            }
            foreach(shape shape in shapes)
            {
                Console.WriteLine(shape.info + $"{shape.info},area={shape.area}");
            }
        }
    }
}
