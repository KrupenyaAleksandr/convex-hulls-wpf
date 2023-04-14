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
    }
}
