using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;

namespace convex_hulls_wpf
{
    internal class painter
    {
        public void BackPattern(Canvas Background)
        {
            double w = Background.ActualWidth;
            double h = Background.ActualHeight;

            Background.Children.Clear();

            int segmentation = -2;
            Label tmp_label;

            for (int x = 15; x < w; x += 30)
                AddLineToBackground(x, 0, x, h, Background);
            for (int y = 15; y < h; y += 30)
                AddLineToBackground(0, y, w, y, Background);
            Line tmp_line = new Line() // осевая линия X
            {
                X1 = 0,
                Y1 = 525,
                X2 = 1000,
                Y2 = 525,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            tmp_line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Background.Children.Add(tmp_line);
            tmp_line = new Line() // осевая линия Y
            {
                X1 = 75,
                Y1 = 600,
                X2 = 75,
                Y2 = 0,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            tmp_line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Background.Children.Add(tmp_line);

            for (int x = 15; x < w; x += 60) // подпись ось X
            {
                tmp_label = new Label()
                {
                    Content = segmentation,
                    Margin = new Thickness(x - 21, 523, 0, 0),
                    FontSize = 14,
                };
                segmentation += 2;
                Background.Children.Add(tmp_label);
            }

            segmentation = 16;
            for (int y = 52; y < h; y += 60) // подпись ось Y
            {
                if (segmentation == 0)
                {
                    segmentation = -2;
                    continue;
                }
                tmp_label = new Label()
                {
                    Content = segmentation,
                    Margin = new Thickness(51, y - 15, w, y),
                    FontSize = 14,
                };
                segmentation -= 2;
                Background.Children.Add(tmp_label);
            }
        }

        internal void AddLineToBackground(double x1, double y1, double x2, double y2, Canvas Background)
        {
            Line line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.LightGray,
                StrokeThickness = 1,
                SnapsToDevicePixels = true
            };
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Background.Children.Add(line);
        }
    }
}
