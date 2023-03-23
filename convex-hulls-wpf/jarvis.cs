using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace convex_hulls_wpf
{
    public class jarvis
    {
        public List<point> build_hull_jarvis(List<point> points)
        {
            List<point> hull = new List<point>();
            point p0 = new point(points[0]);
            foreach (point p in points)
            {
                if (p.x < p0.x || (p.x == p0.x && p.y < p0.y)) p0 = p;
            }
            hull.Add(p0);
            //points.Remove(points[0]);
            while (true)
            {
                point tmp = new point(-999, -999);
                foreach (point p in points) {
                    point _tmp = new point();
                    _tmp = p - p0;
                    _tmp = tmp ^ (tmp - p0);
                    if (((p - p0) ^ (tmp - p0)) > 0)
                    {
                        tmp = p;
                    }
                }
                if (tmp == p0) break;
                else
                {
                    p0 = tmp;
                    hull.Add(tmp);
                }
            }
            return hull;
        }
    }
}

/*r p0 = points[0];
for (r p : points)
    if (p.x < p0.x || (p.x == p0.x && p.y < p0.y))
        p0 = p;
vector<r> hull = { p0 };
while (true)
{
    r t = p0; // кандидат на следующую точку
    for (r p : points)
        // лучше никакие полярные углы не считать
        if ((p - p0) ^ (t - p0) > 0)
            t = p;
    if (t == p0)
        continue;
    else
    {
        p0 = t;
        hull.push_back(t);
    }
}
return hull;*/