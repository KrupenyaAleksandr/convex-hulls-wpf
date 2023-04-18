using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace convex_hulls_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<point> points = new List<point>();
        jarvis _jarvis = new jarvis();
        painter _painter = new painter();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _painter.BackPattern(Background);
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            if (points.Count <= 0) { Console.WriteLine("1"); return; }
            points = _jarvis.jarvismarch(points);
            _painter.Draw_Hull(points, Background);
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            if (points.Count > 0)
            {
                Background.Children.Clear();
                _painter.BackPattern(Background);
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
                points[i].Draw_Point(Background);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this);
            Console.WriteLine("X: {0}, Y: {1}", point.X, point.Y);
        }
    }
}
