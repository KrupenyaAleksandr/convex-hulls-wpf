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
        painter _painter;
        public MainWindow()
        {
            InitializeComponent();
            _painter = new painter(MainGrid, Background);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _painter.Init_UI();
        }

    }
}
