using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

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

        public point(point a)
        {
            this.X = a.x;
            this.Y = a.y;
        }

        public static point operator -(point a, point b)
        {
            return new point(a.x - b.x, a.y - b.y);
        }

        public static point operator ^(point a, point b)
        {
            return new point(a.x * b.x, a.y * b.y);
        }

        public static bool operator >(point a, int b) // hzhz
        {
            return a.x + a.y > b;
        }

        public static bool operator <(point a, int b) // hzhz
        {
            return a.x + a.y < b;
        }

        public static bool operator ==(point a, point b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }

        public static bool operator !=(point a, point b)
        {
            return (a.x != b.x) && (a.y != b.y);
        }

        public void Draw_Point(Canvas canvas)
        {
            Ellipse ellipes = new Ellipse()
            {
                Height = 14,
                Width = 14,
                VerticalAlignment = VerticalAlignment.Bottom, // не работает
                HorizontalAlignment = HorizontalAlignment.Left,
                Fill = Brushes.Red,
                Margin = new Thickness(75 + this.x * 30 - 7, 525 - this.y * 30 - 7, 0, 0),
            };
            canvas.Children.Add(ellipes);
        }
    }
}
