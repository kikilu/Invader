using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Invader
{
    class Invader
    {
        public static readonly Size imagesize = Properties.Resources.bug1.Size;

        private const int HorizontalInterval = 10;
        private const int VerticalInterval = 20;

        private Bitmap image;

        public Point Location { get; private set; }
        public ShipType InvaderType { get; private set; }
        public Rectangle Area { get { return new Rectangle(Location, image.Size); } }
        public int Score { get; private set; }
        public Point originalPoint = new Point();

        #region 敌人种类
        private List<Bitmap> bug = new List<Bitmap>
        {
            new Bitmap(Properties.Resources.bug1),
            new Bitmap(Properties.Resources.bug2),
            new Bitmap(Properties.Resources.bug3),
            new Bitmap(Properties.Resources.bug4),
        };
        private List<Bitmap> flyingSaucer = new List<Bitmap>
        {
            new Bitmap(Properties.Resources.flyingsaucer1),
            new Bitmap(Properties.Resources.flyingsaucer2),
            new Bitmap(Properties.Resources.flyingsaucer3),
            new Bitmap(Properties.Resources.flyingsaucer4),
        };
        private List<Bitmap> satellite = new List<Bitmap>
        {
            new Bitmap(Properties.Resources.satellite1),
            new Bitmap(Properties.Resources.satellite2),
            new Bitmap(Properties.Resources.satellite3),
            new Bitmap(Properties.Resources.satellite4),
        };
        private List<Bitmap> spaceShip = new List<Bitmap>
        {
            new Bitmap(Properties.Resources.spaceship1),
            new Bitmap(Properties.Resources.spaceship2),
            new Bitmap(Properties.Resources.spaceship3),
            new Bitmap(Properties.Resources.spaceship4),
        };
        private List<Bitmap> star = new List<Bitmap>
        {
            new Bitmap(Properties.Resources.star1),
            new Bitmap(Properties.Resources.star2),
            new Bitmap(Properties.Resources.star3),
            new Bitmap(Properties.Resources.star4),
        };
        private List<Bitmap> watchit = new List<Bitmap>
        {
            new Bitmap(Properties.Resources.watchit1),
            new Bitmap(Properties.Resources.watchit2),
            new Bitmap(Properties.Resources.watchit3),
            new Bitmap(Properties.Resources.watchit4),
        };
        #endregion

        public Invader(ShipType invaderType,Point location,int score)
        {
            this.InvaderType = invaderType;
            this.Location = location;
            this.Score = score;
            image = InvaderImage(0);
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:Location = new Point(Location.X - HorizontalInterval, Location.Y);
                    break;
                case Direction.Right:Location = new Point(Location.X + HorizontalInterval, Location.Y);
                    break;
                default:Location = new Point(Location.X, Location.Y + VerticalInterval);
                    break;
            }
            originalPoint.X = Area.X + Area.Width / 2;
            originalPoint.Y = Area.Y + Area.Height / 2;
        }

        public void Draw(Graphics g,int animationCell)
        {
            g.DrawImage(InvaderImage(animationCell), Location);
        }

        private Bitmap InvaderImage(int animationCell)
        {
            switch (InvaderType)
            {
                case ShipType.Bug:image = bug[animationCell];
                    break;
                case ShipType.Saucer:image = flyingSaucer[animationCell];
                    break;
                case ShipType.Satellite:image = satellite[animationCell];
                    break;
                case ShipType.Spaceship:image = spaceShip[animationCell];
                    break;
                case ShipType.Star:image = star[animationCell];
                    break;
                default:
                    break;
            }
            return image;
        }

        public bool Attcaked(Shot playerShot)
        {
            if (playerShot != null)
            {
                if (Math.Abs(originalPoint.X - playerShot.Location.X) <= Area.Width / 2 &&
                    Math.Abs(originalPoint.Y - playerShot.Location.Y) <= Area.Height / 2)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}