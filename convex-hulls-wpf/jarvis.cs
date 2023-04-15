using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace convex_hulls_wpf
{
    public class jarvis
    {
        private double rotate(point a, point b, point c) // функция для определения с какой стороны находится точка C относительно отрезка AB
        {
            return (b.x - a.x) * (c.y - b.y) - (b.y - a.y) * (c.x - b.x); // > 0, левая сторона; < 0, правая сторона
        }

        public List<point> jarvismarch(List<point> points)
        {
            List<int> P = new List<int>();
            for (int i = 0; i < points.Count; ++i) // создаём список номеров, равный количеству точек
            {
                P.Add(i);
            }
            point p0 = new point(); // берём первую попавшуюся точку
            p0 = points[0];
            for (int i = 0; i < points.Count; ++i)
            {
                if (points[i].x < points[0].x) // ищем самую левую точку по иксу
                {
                    point tmp = new point();
                    tmp = points[i];
                    points[i] = points[0]; // меняем местами точки в списке точек, чтобы не попасть сразу на начальную потом
                    points[0] = tmp;
                }
            }
            List<point> hull = new List<point>(); // список точек, которые будут входить в выпуклую оболочку
            hull.Add(points[0]); // сразу добавим первую точку
            points.Remove(points[0]); // переместим первую точку, входящую в оболочку в конец списка точек, чтобы потом,
            points.Add(hull[0]); //  в самом конце наткнутся на неё и завершить алгоритм
            while (true)
            {
                int right = 0;
                for (int i = 0; i < points.Count; ++i)
                {
                    if (rotate(hull[hull.Count - 1], points[right], points[i]) < 0) // если точка находится справа от отрезка, который
                    {                                                                    //  получается от точки hull[hull.count -1] до points[p[right]] выбираем её
                        right = i;
                    }
                }
                if (points[right] == hull[0]) //если наткнулись на самую первую точку, значит прошли вокруг оболочки, выходим из цикла
                {
                    break;
                }
                else
                {
                    hull.Add(points[right]); // добавляем точку, которая оказалась правее нашего отрезка AB
                    points.Remove(points[right]);
                }
            }
            return hull;
        }
    }
}