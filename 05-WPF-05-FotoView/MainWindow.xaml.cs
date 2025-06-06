﻿using Microsoft.Win32;
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

namespace _05_WPF_05_FotoView
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

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Title = "Select picture";
            ofd.Filter =
                "All supported graphics" +
                "|JPEG (*.jpg; *jpeg)|*.jpg;*jpeg" +
                "|PNG (*.png)|*.png";

            if (ofd.ShowDialog() != true)
                return;

            string filename = ofd.FileName;

            FileNameTB.Text = filename;

            try
            {
                Uri imageUri = new(filename);

                PhotoCtrl.Source = new BitmapImage(imageUri);
            }
            catch
            {
                MessageBox.Show("Error opening file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}