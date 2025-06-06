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

namespace _05_WPF_03_MessageBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Klik!", "Super Zpráva", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    break;
                case MessageBoxResult.No:
                    this.Close();
                    break;
                default:
                    break;
            }
        }
    }
}