using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Invader
{
    class PlayerShip
    {
        private Bitmap image;
        private int pixelsToMove = 15;
        private Rectangle rectangle;
        
        public Point Location { get; private set; }
        public int LiveLeft { get; private set; }
        

        public Rectangle Area { get { return new Rectangle(Location, image.Size); } }
        public bool Alive { get; private set; }

        public PlayerShip(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            Alive = true;
            image = Properties.Resources.player;
            Location = new Point(rectangle.Width / 2 - image.Width / 2, rectangle.Height - image.Height - 10);
            LiveLeft = 3;
        }

        public bool Attacked (Shot invaderShot)
        {
            if (Math.Abs(Location.X + image.Width / 2 - invaderShot.Location.X) <= Area.Width / 2 && Math.Abs(Location.Y + image.Height / 2 - invaderShot.Location.Y) <= Area.Height / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GameOver()
        {
            Alive = false;
        }

        public void Draw(Graphics g)
        {
            if (Alive)
                g.DrawImage(image, Area);
            else
            {
                using (Font arial123Bold = new Font("Arial",24,FontStyle.Bold))
                {
                    SizeF size = g.MeasureString("GAME OVER", arial123Bold);
                    g.DrawString("GAME OVER", arial123Bold, Brushes.Red, new Point(rectangle.Width / 2 - (int)size.Width / 2, rectangle.Height / 2 - (int)size.Height / 2));
                }
            }
                
        }

        public void Move(Direction direction)
        {
            switch(direction)
            {
                case Direction.Left:
                    if (Location.X > 5)
                        Location = new Point(Location.X - pixelsToMove, Location.Y);
                    break;
                case Direction.Right:
                    if (Location.X < rectangle.Width - image.Width)
                        Location = new Point(Location.X + pixelsToMove, Location.Y);
                    break;
                default:break;
            }
        }

        public bool Relive()
        {
            if (LiveLeft > 0)
            {
                LiveLeft--;
                Alive = true;
                return true;
            }
            else
            {
                Alive = false;
                return false;
            }
        }
    }
}