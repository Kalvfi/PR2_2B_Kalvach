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
using WpfAnimatedGif;

namespace Projekt_Space_Invaders
{
    public enum ShipType
    {
        Squid,
        Crab,
        Octopus
    }

    public partial class EnemyShip : UserControl
    {
        public ShipType ShipType { get; private set; }
        public int Value { get; private set; }
        public double Speed { get; private set; }

        private Random gen = new Random();

        public Rect Hitbox;

        public EnemyShip(ShipType type)
        {
            InitializeComponent();
            ShipType = type;
            Speed = 0.7;

            Value = ShipType switch
            {
                ShipType.Squid => 30,
                ShipType.Crab => 20,
                ShipType.Octopus => 10
            };

            Width = ShipType switch
            {
                ShipType.Squid => 20,
                ShipType.Crab => 27.5,
                ShipType.Octopus => 30
            };

            SetImage();
        }

        private void SetImage()
        {
            string path = ShipType switch
            {
                ShipType.Squid => "pack://application:,,,/images/squid.gif",
                ShipType.Crab => "pack://application:,,,/images/crab.gif",
                ShipType.Octopus => "pack://application:,,,/images/octopus.gif"
            };

            ImageBehavior.SetAnimatedSource(EnemyImage, new BitmapImage(new Uri(path, UriKind.Absolute)));
        }

        public void SwitchDirection()
        {
            Speed = -Speed;
        }

        public void Move()
        {
            Canvas.SetLeft(this, Canvas.GetLeft(this) + Speed);
            UpdateHitbox();
        }

        public void MoveDown()
        {
            Canvas.SetTop(this, Canvas.GetTop(this) + 10);
            UpdateHitbox();
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rect(
                Canvas.GetLeft(this),
                Canvas.GetTop(this),
                this.Width,
                20);
        }

        public bool IsOutOfBounds(double canvasWidth)
        {
            return Canvas.GetLeft(this) < 0 || Canvas.GetLeft(this) + this.ActualWidth > canvasWidth;
        }
    }
}
