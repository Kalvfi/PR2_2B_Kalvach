using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_WPF_06_Calculator
{
    enum Operation { None, Add, Subtract, Multiply, Divide }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Operation lastOperation = Operation.None;
        private double lastNum;
        private string decimalDot = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;


        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayTextProperty =
            DependencyProperty.Register("DisplayText", typeof(string), typeof(MainWindow), new PropertyMetadata(""));



        public MainWindow()
        {
            InitializeComponent();
            DisplayText = "0";
        }

        private void NumBtnClick(object sender, RoutedEventArgs e)
        {
            string digit = ((Button)sender).Content.ToString()!;

            if (DisplayText == "0")
                DisplayText = digit;
            else
                DisplayText += digit;
        }

        private void DecimalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!DisplayText.Contains(decimalDot))
                DisplayText += decimalDot;
        }

        private void ACBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayText = "0";
        }

        private void MinusPlusBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayText = (double.Parse(DisplayText)*(-1)).ToString();
        }

        private void PercentBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayText = (double.Parse(DisplayText) / 100).ToString();
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            string symbol = ((Button)sender).Content.ToString()!;
            Operation operation = symbol switch
            {
                "+" => Operation.Add,
                "-" => Operation.Subtract,
                "×" => Operation.Multiply,
                "÷" => Operation.Divide,
                _ => Operation.None
            };

            if (lastOperation != Operation.None)
            {
                double currentNum = double.Parse(DisplayText);
                lastNum = Calculate(currentNum);
            }

            lastOperation = operation;
            lastNum = double.Parse(DisplayText);
            DisplayText = "0";
        }

        private void EqualsBtn_Click(object sender, RoutedEventArgs e)
        {
            double currentNum = double.Parse(DisplayText);
            double result = Calculate(currentNum);

            DisplayText = result.ToString();
            lastOperation = Operation.None;
        }

        private double Calculate(double currentNum)
        {
            return lastOperation switch
            {
                Operation.Add => SimpleMath.Add(lastNum, currentNum),
                Operation.Subtract => SimpleMath.Subtract(lastNum, currentNum),
                Operation.Multiply => SimpleMath.Multiply(lastNum, currentNum),
                Operation.Divide => SimpleMath.Divide(lastNum, currentNum),
                _ => 0
            };
        }
    }
}