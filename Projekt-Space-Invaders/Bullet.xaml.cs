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

namespace Projekt_Space_Invaders
{
    /// <summary>
    /// Interaction logic for Bullet.xaml
    /// </summary>
    public partial class Bullet : UserControl
    {
        public bool IsFromPlayer { get; private set; }
        public double Speed => IsFromPlayer ? -7.5 : 7.5;
        public Rect Hitbox;

        public Bullet(bool isFromPlayer)
        {
            InitializeComponent();
            IsFromPlayer = isFromPlayer;

            BulletBody.Fill = isFromPlayer ? Brushes.White : Brushes.Yellow;
        }

        public void Move()
        {
            Canvas.SetTop(this, Canvas.GetTop(this) + Speed);
            UpdateHitbox();
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rect(
                Canvas.GetLeft(this),
                Canvas.GetTop(this),
                this.ActualWidth,
                this.ActualHeight);
        }

        public bool IsOutOfBounds(double canvasHeight)
        {
            return Canvas.GetTop(this) < 0 || Canvas.GetTop(this) + this.ActualHeight > canvasHeight;
        }
    }
}
