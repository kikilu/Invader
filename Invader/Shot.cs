using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Invader
{
    class Shot
    {
        private const int moveInterval = 10;
        public static Size ShotSize = new Size(2, 10);

        public Point Location { get; private set; }

        private Direction direction;
        private Rectangle boundaries;

        public Shot(Point location,Direction direction,Rectangle boundaries)
        {
            this.Location = location;
            this.direction = direction;
            this.boundaries = boundaries;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.White, new Rectangle(Location, ShotSize));
        }

        public bool Move(Direction direction)
        {
            switch(direction)
            {
                case Direction.Up:
                        if (Location.Y - moveInterval > 0)
                        {
                            Location = new Point(Location.X, Location.Y - moveInterval);
                            return true;
                        }
                    break;
                case Direction.Down:
                    if (Location.Y + moveInterval < boundaries.Height)
                    {
                        Location = new Point(Location.X, Location.Y + moveInterval);
                        return true;
                    }
                    break;
                default:break;
            }
            
            return false;
        }
    }
}