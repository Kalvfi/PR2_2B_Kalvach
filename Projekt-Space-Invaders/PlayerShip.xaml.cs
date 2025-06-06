using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
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

namespace Projekt_Space_Invaders
{
    /// <summary>
    /// Interaction logic for PlayerShip.xaml
    /// </summary>
    public partial class PlayerShip : UserControl
    {
        public double Speed { get; private set; } = 7.5;
        public Rect Hitbox { get; private set; }

        public PlayerShip()
        {
            InitializeComponent();
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rect(
                Canvas.GetLeft(this),
                Canvas.GetTop(this),
                this.ActualWidth,
                this.ActualHeight);
        }
    }
}
