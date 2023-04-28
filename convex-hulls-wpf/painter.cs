using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Reflection;

namespace convex_hulls_wpf
{
    internal class painter
    {
        Canvas _canvas;
        Grid _grid;
        List<point> points = new List<point>();

        int start = 0;
        int finish = 0;
        bool finished = false;

        TextBox Input = new TextBox()
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            TextWrapping = TextWrapping.NoWrap,
            Text = "",
            Width = 160,
            Height = 20,
            Margin = new Thickness(40, 35, 0, 0),
        };

        TextBox Output = new TextBox()
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            TextWrapping = TextWrapping.NoWrap,
            IsReadOnly = true,
            Text = "",
            Width = 160,
            Height = 20,
            Margin = new Thickness(800, 35, 0, 0),
        };

        Label Answer = new Label()
        {
            FontSize = 14,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Content = "Ответ",
            Margin = new Thickness(750, 30, 0, 0),
        };

        Button Build = new Button()
        {
            Content = "Построить",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(480, 35, 0, 0),
        };

        Button Draw_points = new Button()
        {
            Content = "Добавить точки",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(350, 35, 0, 0),
        };

        Button Forward = new Button()
        {
            Content = "Вперёд",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(390, 35, 0, 0),
        };

        ComboBox Options = new ComboBox()
        {
            ItemsSource = new string[]
            {
                "Jarvis",
                "Graham"
            },
            SelectedItem = "Jarvis",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(250, 35, 0, 0),
        };
        

        public painter(Grid grid, Canvas canvas) {
            _grid = grid;
            _canvas = canvas;
        }

        public void Init_UI()
        {
            _grid.Children.Add(Input);
            Draw_points.Click += Draw_Click;
            _grid.Children.Add(Draw_points);
            Build.Click += Build_Click;
            _grid.Children.Add(Build);
            Forward.Click += Forward_Click;
            _grid.Children.Add(Output);
            _grid.Children.Add(Answer);
            _grid.Children.Add(Options);
            BackPattern();
        }

        public void BackPattern()
        {
            double w = _canvas.ActualWidth;
            double h = _canvas.ActualHeight;

            _canvas.Children.Clear();

            int segmentation = -2;
            Label tmp_label;

            for (int x = 15; x < w; x += 30)
                AddLineToBackground(x, 0, x, h);
            for (int y = 15; y < h; y += 30)
                AddLineToBackground(0, y, w, y);
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
            _canvas.Children.Add(tmp_line);
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
            _canvas.Children.Add(tmp_line);

            for (int x = 15; x < w; x += 60) // подпись ось X
            {
                tmp_label = new Label()
                {
                    Content = segmentation,
                    Margin = new Thickness(x - 21, 523, 0, 0),
                    FontSize = 14,
                };
                segmentation += 2;
                _canvas.Children.Add(tmp_label);
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
                _canvas.Children.Add(tmp_label);
            }
        }

        internal void Draw_Hull(List <point> points)
        {
            _grid.Children.Add(Forward);
            start = 0;
            finish = points.Count();
            finished = false;
        }

        public void Draw_Point(point point)
        {
            Ellipse ellipes = new Ellipse()
            {
                Height = 14,
                Width = 14,
                VerticalAlignment = VerticalAlignment.Bottom, // не работает
                HorizontalAlignment = HorizontalAlignment.Left,
                Fill = Brushes.Red,
                Margin = new Thickness(75 + point.x * 30 - 7, 525 - point.y * 30 - 7, 0, 0),
            };
            _canvas.Children.Add(ellipes);
        }

        internal void AddLineToBackground(double x1, double y1, double x2, double y2)
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
            _canvas.Children.Add(line);
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            if (points.Count > 0)
            {
                _canvas.Children.Clear();
                BackPattern();
                points.Clear();
            }
            string input = Input.GetLineText(0);
            string[] tmp_nums = new string[2];
            double[] nums = new double[2];

            NumberFormatInfo _numberformat = new NumberFormatInfo() // формат для обработки двоичных чисел с точкой в качестве разделителя 
            {
                NumberDecimalSeparator = ".",
            };

            foreach (string line in input.Split(';'))
            {
                if (line == "") break;
                tmp_nums = line.Split(',');
                try
                {
                    nums[0] = double.Parse(tmp_nums[0], _numberformat);
                }
                catch (Exception)
                {
                    return;
                }
                try
                {
                    nums[1] = double.Parse(tmp_nums[1], _numberformat);
                }
                catch (Exception)
                {
                    return;
                }
                points.Add(new point(nums[0], nums[1]));
            }

            for (int i = 0; i < points.Count; ++i)
                Draw_Point(points[i]);
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            if (points.Count <= 0) return;
            bypass _bypass = new bypass();
            if ((string)Options.SelectedItem == "Jarvis") points = _bypass.jarvismarch(points);
            else points = _bypass.grahammarch(points);
            _grid.Children.Remove(Build);
            _grid.Children.Remove(Draw_points);
            _grid.Children.Remove(Options);
            Draw_Hull(points);
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            Line line;
            if (finished) return;
            if (start == finish - 1)
            {
                line = new Line()
                {
                    X1 = 75 + points[finish - 1].x * 30,
                    Y1 = 525 - points[finish - 1].y * 30,
                    X2 = 75 + points[0].x * 30,
                    Y2 = 525 - points[0].y * 30,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1
                };
                _canvas.Children.Add(line);
                _grid.Children.Remove(Forward);
                _grid.Children.Add(Build);
                _grid.Children.Add(Draw_points);
                _grid.Children.Add(Options);
                Output.Text = "";
                for (int i = 0; i < points.Count; ++i)
                {
                    Output.Text += points[i].x;
                    Output.Text += ",";
                    Output.Text += points[i].y;
                    Output.Text += ";";
                }
                finished = true;
                return;
            }

            line = new Line()
            {
                X1 = 75 + points[start].x * 30,
                Y1 = 525 - points[start].y * 30,
                X2 = 75 + points[start + 1].x * 30,
                Y2 = 525 - points[start + 1].y * 30,
                Stroke = Brushes.Blue,
                StrokeThickness = 1
            };
            _canvas.Children.Add(line);
            start++;
        }
    }
}