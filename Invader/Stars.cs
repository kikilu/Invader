using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Invader
{
    class Stars
    {
        List<Star> stars = new List<Star>();
        private Rectangle rectangle;
        private Random random;
        private List<Brush> brushes = new List<Brush>
        {
            Brushes.Red,
            Brushes.White,
            Brushes.Purple,
            Brushes.Yellow,
            Brushes.GreenYellow,
        };

        public Stars (Rectangle rectangle,Random random)
        {
            this.rectangle = rectangle;
            this.random = random;
            for (int i = 0; i < 300; i++)
            {
                stars.Add(new Star(new Point(random.Next(rectangle.Width),
                    random.Next(rectangle.Height)),
                    brushes[random.Next(5)]));
            }
        }


        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Black, rectangle);

            foreach (Star star in stars)
            {
                g.FillEllipse(star.brush, new Rectangle(star.point, new Size(2, 2)));
            }
        }

        public void Twinkle()
        {
            for (int i = 0; i < 5; i++)
            {
                stars[random.Next(300)] = new Star(new Point(random.Next(rectangle.Width),
                    random.Next(rectangle.Height)),
                    brushes[random.Next(5)]);
            }
        }
    }
}