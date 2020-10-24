using System;
using System.Collections.Generic;
using System.Text;

namespace shapes
{
    interface shape
    {
        string info {  get; }
        double area {  get; }
        bool verify();
    }
}
