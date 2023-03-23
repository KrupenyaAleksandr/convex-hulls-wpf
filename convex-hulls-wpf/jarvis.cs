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