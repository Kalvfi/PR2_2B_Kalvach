﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_WPF_01_Hello_world;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool isHello = true;


    public MainWindow()
    {
        InitializeComponent();
        InputTB.Focus();
    }

    private void MoveBtn_Click(object sender, RoutedEventArgs e)
    {
        MoveText();
    }

    private void MoveText()
    {
        OutputTB.Text = InputTB.Text;
        InputTB.Text = string.Empty;
        InputTB.Focus();
    }

    private void InputTB_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) 
        {
            MoveText();
        }
    }
}