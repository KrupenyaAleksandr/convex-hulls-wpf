using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace convex_hulls_wpf
{
    internal class bypass
    {
        private double rotate(point a, point b, point c)
        {
            return (b.x - a.x) * (c.y - b.y) - (b.y - a.y) * (c.x - b.x);
        }

        public List<point> jarvismarch(List<point> points)
        {
            for (int i = 1; i < points.Count; ++i)
            {
                if (points[i].x < points[0].x)
                {
                    point tmp = new point();
                    tmp = points[i];
                    points[i] = points[0]; 
                    points[0] = tmp;
                }
            }
            List<point> hull = new List<point>(); 
            hull.Add(points[0]);
            points.Remove(points[0]);
            points.Add(hull[0]); 
            while (true)
            {
                int right = 0;
                for (int i = 0; i < points.Count; ++i)
                {
                    if (rotate(hull[hull.Count - 1], points[right], points[i]) < 0)
                    {
                        right = i;
                    }
                }
                if (points[right] == hull[0])
                {
                    break;
                }
                else
                {
                    hull.Add(points[right]);
                    points.Remove(points[right]);
                }
            }
            return hull;
        }

        public List<point> grahammarch(List<point> points)
        {
            for (int i = 1; i < points.Count; ++i)
            {
                if (points[i].x < points[0].x) 
                {
                    point tmp = new point();
                    tmp = points[i];
                    points[i] = points[0];
                    points[0] = tmp;
                }
            }
            for (int i = 2; i < points.Count; ++i) 
            {
                int j = i; 
                while (j > 1 && (rotate(points[0], points[j - 1], points[j]) < 0))
                {                               
                    point tmp = new point();     
                    tmp = points[j];              
                    points[j] = points[j - 1];     
                    points[j - 1] = tmp;           
                    j--;
                }
            }
            List<point> hull = new List<point> 
                    {
                        points[0],
                        points[1]
                    };
            for (int i = 2; i < points.Count; ++i)
            {
                while (rotate(hull[hull.Count - 2], hull[hull.Count - 1], points[i]) < 0) 
                {                                                                         
                    hull.RemoveAt(hull.Count - 1);
                }                                                                   
                hull.Add(points[i]);
            }
            return hull;
        }
    }
}