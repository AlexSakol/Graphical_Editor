//ОБРАБОТЧИКИ ГЛАВНОГО ОКНА
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace LAB_5_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Shape? shape;
        public MainWindow()
        {
            InitializeComponent();

            CommandBinding commandBindingHelp = new CommandBinding();
            commandBindingHelp.Command = ApplicationCommands.Help;
            commandBindingHelp.Executed += help;
            MenuItem_Help.CommandBindings.Add(commandBindingHelp);

            CommandBinding commandBindingOpen = new CommandBinding();
            commandBindingOpen.Command = ApplicationCommands.Open;
            commandBindingOpen.Executed += open;
            MenuItem_Open.CommandBindings.Add(commandBindingOpen);

            CommandBinding commandBindingSave = new CommandBinding();
            commandBindingSave.Command = ApplicationCommands.Save;
            commandBindingSave.Executed += save;
            commandBindingSave.CanExecute += save_canExecute;
            MenuItem_Save.CommandBindings.Add(commandBindingSave);

            CommandBinding commandBindingExit = new CommandBinding();
            commandBindingExit.Command = ApplicationCommands.Close;
            commandBindingExit.Executed += close;
            MenuItem_Exit.CommandBindings.Add(commandBindingExit);
        }

        private void close(object sender, ExecutedRoutedEventArgs e) => mainWindow.Close();

        private void save_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(shape is not null) e.CanExecute = true;
        }

        private void save(object sender, ExecutedRoutedEventArgs e)
        {
            if (shape != null) shape.Save();
            else return;
        }

        async private void open(object sender, ExecutedRoutedEventArgs e) => shape = await Shape.Load();

        private void help(object sender, ExecutedRoutedEventArgs e)=> MessageBox.Show("Help!");
        

        private void MenuItem_Shape_Click(object sender, RoutedEventArgs e)
        {
            Window_Shape window_Shape = new();
            if (window_Shape.ShowDialog() == true)
            {
                shape = window_Shape.getShape();
                MessageBox.Show(shape.ToString());
            }
            else return;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (shape != null)
            {
                shape.Draw(canvas, e.GetPosition(canvas));
            }
            else return;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            textBlockCursor.Text = $"X = {e.GetPosition(canvas).X} Y = {e.GetPosition(canvas).Y}";
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => mainWindow.Close();

        private void Help_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Help!");

        private void Shape_Click(object sender, RoutedEventArgs e)
        {
            Window_Shape window_Shape = new();
            if (window_Shape.ShowDialog() == true)
            {
                shape = window_Shape.getShape();
                MessageBox.Show(shape.ToString());
            }
            else return;
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            if (shape != null) shape.Save();
            else return;
        }
                
        // загрузка по нажатии на правую кнопку мыши
        async private void File_Load(object sender, MouseButtonEventArgs e)=> shape = await Shape.Load();

        private void File_Save(object sender, RoutedEventArgs e)
        {//сохранение по нажатии на левую кнопку мыши
            if (shape != null) shape.Save();
            else return;
        }
       
    }
}
