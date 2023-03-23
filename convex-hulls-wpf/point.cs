using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace convex_hulls_wpf
{
    public class point
    {
        private double X;
        private double Y;
        public double x
        {
            set { this.X = value; }
            get { return this.X; }
        }
        public double y
        {
            set { this.Y = value; }
            get { return this.Y; }
        }

        public point(double x, double y) { 
            this.X = x;
            this.Y = y;
        }

        public point() { }

        public static point operator -(point a, point b)
        {
            return new point(a.x - b.x, a.y - b.y);
        }

        public static point operator ^(point a, point b)
        {
            return new point(a.x * b.x, a.y * b.y);
        }
    }
}
