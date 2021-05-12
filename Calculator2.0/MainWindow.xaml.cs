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

namespace Calculator2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void NumPadInput(string digit)
        {
            if (equaled)
            {
                CalculatorScreen.Text = digit;
                CalculatorHistory.Text = "";
                equaled = false;
            }
            else if (CalculatorScreen.Text == "0")
                CalculatorScreen.Text = digit;
            else if (CalculatorScreen.Text.Length < 21)
            {
                CalculatorScreen.Text += digit;
                //if ((CalculatorScreen.Text.Length - 1) % 3 == 0 && CalculatorScreen.Text.Length > 1)
                //    CalculatorScreen.Text = CalculatorScreen.Text.Insert(1, ",");
            }
        }
        public void CheckEqual(string sign)
        {
            if (equaled == false)
            {
                if (CalculatorHistory.Text.Length + CalculatorScreen.Text.Length >= 30)
                    CalculatorHistory.Text = $" {currentNum} {sign}";
                else    
                    CalculatorHistory.Text += $" {CalculatorScreen.Text} {sign}";
            }
            else
            {
                CalculatorHistory.Text = $"{currentNum} {sign}";
                equaled = false;
            }
        }

        public void CheckPreviousOperation()
        {

            if (addition)
                currentNum += Convert.ToDecimal(CalculatorScreen.Text);
            else if (subtraction)
                currentNum -= Convert.ToDecimal(CalculatorScreen.Text);
            else if (multiplication)
                currentNum *= Convert.ToDecimal(CalculatorScreen.Text);
            else if (division)
                currentNum /= Convert.ToDecimal(CalculatorScreen.Text);

            addition = false; subtraction = false; multiplication = false; division = false;
        }

        decimal currentNum = 0;
        static bool addition = false, subtraction = false, equaled = false, multiplication = false, division = false;
        static bool previouslyOperated = false;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void AdditionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalculatorScreen.Text == "")
                CalculatorScreen.Text = "0";


            if (previouslyOperated)
                CheckPreviousOperation();
            else
                try
                {
                    currentNum += Convert.ToDecimal(CalculatorScreen.Text);
                }
                catch (FormatException)
                {
                    CalculatorScreen.Text = "0";
                    currentNum += Convert.ToDecimal(CalculatorScreen.Text);
                }

            CheckEqual("+");

            CalculatorScreen.Text = "";
            addition = true;
            previouslyOperated = true;
        }
        private void DivisionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalculatorScreen.Text == "")
                CalculatorScreen.Text = "0";
            else if ((CalculatorHistory.Text == "" || equaled) && CalculatorScreen.Text != "Cannot divide by zero")
                currentNum = Convert.ToDecimal(CalculatorScreen.Text);
            else if (previouslyOperated)
                CheckPreviousOperation();
            else
                try
                {
                    currentNum /= Convert.ToDecimal(CalculatorScreen.Text);
                }
                catch (DivideByZeroException)
                {
                    CalculatorScreen.Text = "Cannot divide by zero";
                    equaled = true;
                    currentNum = 0;
                    previouslyOperated = false; addition = false; subtraction = false; multiplication = false; division = false;
                }
                catch (FormatException)
                {
                    CalculatorScreen.Text = "0";
                }

            CheckEqual("÷");


            CalculatorScreen.Text = "";
            division = true;
            previouslyOperated = true;
        }
        private void MultiplicationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CalculatorScreen.Text == "")
                CalculatorScreen.Text = "0";
            else if ((CalculatorHistory.Text == "" || equaled) && CalculatorScreen.Text != "Cannot divide by zero")
                currentNum = Convert.ToDecimal(CalculatorScreen.Text);
            else if (previouslyOperated)
                CheckPreviousOperation();
            else
                try
                {
                    currentNum *= Convert.ToDecimal(CalculatorScreen.Text);
                }
                catch (FormatException)
                {
                    CalculatorScreen.Text = "0";
                    currentNum *= Convert.ToDecimal(CalculatorScreen.Text);
                }

            CheckEqual("x");


            CalculatorScreen.Text = "";
            multiplication = true;
            previouslyOperated = true;
        }
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            CalculatorHistory.Text += $" {CalculatorScreen.Text} =";
            equaled = true;

            if (addition)
                CalculatorScreen.Text = (currentNum + Convert.ToDecimal(CalculatorScreen.Text)).ToString();
            else if (subtraction)
                CalculatorScreen.Text = (currentNum - Convert.ToDecimal(CalculatorScreen.Text)).ToString();
            else if (multiplication)
                CalculatorScreen.Text = (currentNum * Convert.ToDecimal(CalculatorScreen.Text)).ToString();
            else if (division)
            {
                try
                {
                    CalculatorScreen.Text = (currentNum / Convert.ToDecimal(CalculatorScreen.Text)).ToString();
                }
                catch (DivideByZeroException)
                {
                    CalculatorScreen.Text = "Cannot divide by zero";
                }
            }
            previouslyOperated = false; addition = false; subtraction = false; multiplication = false; division = false;
            currentNum = 0;
        }
        
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (equaled)
                CalculatorHistory.Text = "";
            else if (CalculatorScreen.Text != "" && CalculatorScreen.Text != "0")
            {
                CalculatorScreen.Text = CalculatorScreen.Text.Remove(CalculatorScreen.Text.Length - 1, 1);
                if (CalculatorScreen.Text == "")
                    CalculatorScreen.Text = "0";
            }
        }
        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            currentNum = 0;
            CalculatorHistory.Text = "";
            CalculatorScreen.Text = "0";
            addition = false; subtraction = false; equaled = false;
        }
        private void ClearExpressionButton_Click(object sender, RoutedEventArgs e)
        {
            CalculatorScreen.Text = "0";
        }


        private void Num0Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("0");
        }
        private void Num1Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("1");
        }
        private void Num2Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("2");
        }
        private void Num3Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("3");
        }
        private void Num4Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("4");
        }
        private void Num5Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("5");
        }
        private void Num6Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("6");
        }
        private void Num7Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("7");
        }
        private void Num8Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("8");
        }
        private void Num9Button_Click(object sender, RoutedEventArgs e)
        {
            NumPadInput("9");
        }
    }
}
