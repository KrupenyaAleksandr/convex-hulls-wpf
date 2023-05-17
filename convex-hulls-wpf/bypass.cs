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
        private double rotate(point a, point b, point c) // функция для определения с какой стороны находится точка C относительно отрезка AB
        {
            return (b.x - a.x) * (c.y - b.y) - (b.y - a.y) * (c.x - b.x); // > 0, левая сторона; < 0, правая сторона
        }

        public List<point> jarvismarch(List<point> points)
        {
            for (int i = 1; i < points.Count; ++i)
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

        public List<point> grahammarch(List<point> points)
        {
            for (int i = 1; i < points.Count; ++i)
            {
                if (points[i].x < points[0].x) // ищем самую левую точку по иксу
                {
                    point tmp = new point();
                    tmp = points[i];
                    points[i] = points[0];
                    points[0] = tmp;
                }
            }
            for (int i = 2; i < points.Count; ++i) // сортировка вставками, точки располагаются сверхну вниз 
            {
                int j = i; 
                while (j > 1 && (rotate(points[0], points[j - 1], points[j]) < 0)) // пока не дошли до начала списка или пока точка оказывается справа от отрезка
                {                                  // нам надо упорядочить список, мы строим отрезки от поинт[нулевая] до точки поинт[ЖИТАЯ - 1]
                    point tmp = new point();       // так как все точки должны быть снизу вверх и мы строим отрезок от гарантированно самой левой точке на плоскости
                    tmp = points[j];               // то получается, что если поинтс[житая], то есть текущая точка в форе "забежала вперёд списка" и оказалась правее чем поинт[ЖИТАЯ - 1] которая должна быть
                    points[j] = points[j - 1];     // ниже неё, или правее относительно отрезка от поинт[0], значит нам надо поменять их местами (если я забуду и будет тяжело понять просто надо наклонить плоскость вправо)
                    points[j - 1] = tmp;           
                    j--;
                }
            }
            List<point> hull = new List<point> // добавляем в список две точки, которые гарантированно входят в МВО 
                    {
                        points[0],
                        points[1]
                    };
            for (int i = 2; i < points.Count; ++i) // обрезаем углы
            {
                while (rotate(hull[hull.Count - 2], hull[hull.Count - 1], points[i]) < 0) // если поинт[итая] находится правее отрезка, то удаляем последнюю точку из списка точек которые в МВО до тех пор, 
                {                                                                         
                    hull.RemoveAt(hull.Count - 1);
                }                                                                    // пока точка итая перестанет быть правее отрезка и после этого добавляем в МВО поинт[итую] точку
                hull.Add(points[i]);
            }
            return hull;
        }
    }
}