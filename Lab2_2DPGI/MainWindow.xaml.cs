using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;

namespace Lab2_2DPGI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            CommandBindings.Add(saveCommand);
            CommandBinding openCommand = new CommandBinding(ApplicationCommands.Open, execute_Open, canExecute_Open);
            CommandBindings.Add(openCommand);
            CommandBinding clearCommand = new CommandBinding(ApplicationCommands.Delete, execute_Clear, canExecute_Clear);
            CommandBindings.Add(clearCommand);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1()
        {

        }

        private void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }

        private void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = Assembly.GetEntryAssembly().Location;
            save.Filter = "Text files (*.txt)|*.txt";
            if (save.ShowDialog() == true)
            {
                string data = inputTextBox.Text.Trim();
                byte[] info = new UTF8Encoding(true).GetBytes(data);
                FileStream stream = (FileStream)save.OpenFile();
                stream.Write(info, 0, info.Length);
            }
        }

        private void canExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text files (*.txt)|*.txt";
            string dir = Assembly.GetEntryAssembly()?.Location;
            if (dir != null)
                open.InitialDirectory = dir;
            if (open.ShowDialog() == true)
                inputTextBox.Text = File.ReadAllText(open.FileName);
        }

        private void canExecute_Clear(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }

        private void execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Text = "";
        }
    }
}

