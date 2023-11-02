using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Keyboard
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        int rand;
        int fails = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        public char[] СharacterSelection()
        {
            double level = Level.Value;
            char[] Symbols = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'x', 'z', 'c', 'v', 'b', 'n', 'm' };
            char[] newSymbols = new char[(int)level];
            int count = 0;

            while (count != level)
            {
                rand = random.Next(0, 26);
                if (newSymbols[count] == Symbols[rand])
                {
                    while (newSymbols[count] != Symbols[rand])
                    {
                        rand = random.Next(0, 26);
                    }

                }
                newSymbols[count] = Symbols[rand];
                count++;
            }
            return newSymbols;
        }


        private void ScreenOutput(object sender, RoutedEventArgs e)
        {
            Input.Text = null;
            Output.Text = null;
            Output.IsReadOnly = false;
            fails = 0;

            char[] mass = СharacterSelection();
            int count = 0;
            int tempRnd;
            int temp = 0;

            Output.Focus();
            Level.IsEnabled = false;
            Stop.IsEnabled = true;
            Start.IsEnabled = false;
            if (mass.Length > 0)
            {
                while (count < 60)
                {
                    rand = random.Next(0, mass.Length);
                    if (temp == rand) //Старание максимально разнообразных символов
                    {
                        rand = random.Next(0, mass.Length);
                    }
                    temp = rand;
                    Input.Text += mass[rand];

                    tempRnd = random.Next(0, 4);
                    if (tempRnd == 3)
                    {
                        Input.Text += " ";
                    }

                    count++;
                }
            }

        }

        private void StopButton_Click_1(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            Output.IsReadOnly = true;
            Level.IsEnabled = true;
            index = 0;
        }

        int index = 0;
        char[] array;
        private void Output_PreviewKeyDown(object sender, KeyEventArgs e)// Сделать при вводе символа обозначение его на экране
        {
            array = Input.Text.ToCharArray();
            if (e.Key != Key.Back && e.Key != Key.LWin && e.Key != Key.CapsLock && e.Key != Key.LeftAlt && e.Key != Key.RightAlt && e.Key != Key.Tab && e.Key != Key.LeftShift && e.Key != Key.RightShift && e.Key != Key.LeftCtrl && e.Key != Key.RightCtrl)
            {
                if (index < array.Length)
                {
                    if (e.Key.ToString().ToLower() != array[index].ToString() && e.Key != Key.Space)
                    {
                        fails++;
                    }
                    if (e.Key == Key.Space && array[index] != ' ')
                    {
                        fails++;
                    }
                    Fails.Text = fails.ToString();
                    index++;
                }
                else
                    StopButton_Click_1(sender,e); array = null;
            }
            else
                e.Handled = true;
        }
    }
}
