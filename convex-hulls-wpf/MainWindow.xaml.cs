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
        public MainWindow()
        {
            InitializeComponent();
            //UpdateBackPattern(null, null);
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            string input = Input.GetLineText(0);
            string[] nums = new string[2];
            
            foreach (string line in input.Split(';'))
            {
                if (line == "") break;
                nums = line.Split(',');
                points.Add(new point(Convert.ToDouble(nums[0]), Convert.ToDouble(nums[1])));
            }

            _jarvis.jarvismarch(points);
        }

/*        void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            var w = Background.ActualWidth;
            var h = Background.ActualHeight;

            Background.Children.Clear();
            for (int x = 10; x < w; x += 20)
                AddLineToBackground(x, 0, x, h);
            for (int y = 10; y < h; y += 20)
                AddLineToBackground(0, y, w, y);
        }

        void AddLineToBackground(double x1, double y1, double x2, double y2)
        {
            var line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                SnapsToDevicePixels = true
            };
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            Background.Children.Add(line);
        }*/
    }
}
