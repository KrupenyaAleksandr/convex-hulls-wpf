using System;
using System.Collections.Generic;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateBackPattern();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            _jarvis.jarvismarch(points);
        }

        void UpdateBackPattern()
        {
            double w = Background.ActualWidth;
            double h = Background.ActualHeight;

            Background.Children.Clear();
            for (int x = 15; x < w; x += 30)
                AddLineToBackground(x, 0, x, h);
            for (int y = 15; y < h; y += 30)
                AddLineToBackground(0, y, w, y);
            Line tmp = new Line()
            {
                X1 = 0,
                Y1 = 525,
                X2 = 1000,
                Y2 = 525,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            tmp.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Background.Children.Add(tmp);
            tmp = new Line()
            {
                X1 = 75,
                Y1 = 600,
                X2 = 75,
                Y2 = 0,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            tmp.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Background.Children.Add(tmp);

            //for ()
        }

        void AddLineToBackground(double x1, double y1, double x2, double y2)
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this);
            Console.WriteLine("X: {0}, Y: {1}", point.X, point.Y);
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            string input = Input.GetLineText(0);
            string[] nums = new string[2];

            foreach (string line in input.Split(';'))
            {
                if (line == "") break;
                nums = line.Split(',');
                points.Add(new point(Convert.ToDouble(nums[0]), Convert.ToDouble(nums[1])));
            }

            for (int i = 0; i < points.Count; ++i) 
                points[i].Draw_Point(Background);
        }
    }
}
