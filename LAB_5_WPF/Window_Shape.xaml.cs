// ЗАОКОННЫЙ КОД ДЛЯ ОКНА СВОЙСТВ ФИГУРЫ
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
using System.Windows.Shapes;

namespace LAB_5_WPF
{
    /// <summary>
    /// Логика взаимодействия для Window_Shape.xaml
    /// </summary>
    public partial class Window_Shape : Window
    {
        Shape shape;
        public Window_Shape()
        {
            InitializeComponent();
            shape = new Shape(1, Colors.GreenYellow, Colors.DarkGoldenrod, 100, 100);
            grid.DataContext =shape;
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e) => DialogResult = true;
        

        private void Button_Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        
        internal Shape getShape() => shape;
    }
}
