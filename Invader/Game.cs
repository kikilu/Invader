using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invader
{
    class Game
    {
        private int score = 0;              //分数
        private int wave = 0;               //敌人攻击次数
        private int framesSkipped = 0;      //跳帧，减慢入侵者的行动
        private int maximumPlayerShots = 30; //玩家所能发射的最大炮弹数
        private int maxmumInvaderSohts = 3; //敌人所能发射的最大炮弹数

        private Rectangle boundries;        //游戏边界
        private Random random = new Random(); 

        //private Direction invaderDirection; //敌人移动方向
        private List<Invader> invaders = new List<Invader>();   //敌人列表
        private List<Shot> playerShots = new List<Shot>();      //玩家炮弹记录列表
        private List<Shot> invaderShots = new List<Shot>();     //敌人炮弹记录列表


        private Direction direction;
        private bool moveDown = false;

        public Game(Rectangle boundries)
        {
            this.boundries = boundries;
            stars = new Stars(boundries, random);
            playerShip = new PlayerShip(boundries);
            direction = Direction.Right;
            generateInvaders();
        }

        private Stars stars;

        private PlayerShip playerShip;  //玩家飞船
        



        //public event EventHandler GameOver; //事件，游戏结束

        private void generateInvaders()
        {
            int width = (int)Invader.imagesize.Width / 2;
            int height = (int)Invader.imagesize.Height / 2;
            Point originalPoint = new Point(boundries.Width / 2 - 3 * (Invader.imagesize.Width +  2 * width),
                Invader.imagesize.Height * 2);

            for (int i = 1; i < 6; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    invaders.Add(new Invader((ShipType)i - 1, 
                        new Point(originalPoint.X + j * (Invader.imagesize.Width + width),
                        originalPoint.Y - i * (Invader.imagesize.Height + height)), 
                        i * 10));
                }
            }
        }

        public void Draw(Graphics g,int animationCell)
        {
            stars.Draw(g);
            playerShip.Draw(g);
            g.DrawImage(Properties.Resources.player, new Point(boundries.Width - 150, 0));
            using (Font arial24bold = new Font("Arial", 18, FontStyle.Bold))
            {
                g.DrawString("Score: " + score.ToString(), arial24bold, Brushes.White, boundries.Location);
                g.DrawString("  X " + playerShip.LiveLeft, arial24bold, Brushes.Yellow, new Point(boundries.Width - 100, 0));
            }
            foreach (Invader invader in invaders)
            {
                invader.Draw(g, animationCell);
            }
            foreach (Shot shot in playerShots)
            {
                shot.Draw(g);
            }
            foreach (Shot shot in invaderShots)
            {
                shot.Draw(g);
            }
        }

        public void Twinkle()
        {
            stars.Twinkle();
        }

        public void MovePlayer(Direction direction)
        {
            playerShip.Move(direction);
        }

        public void FireShot()
        {
            if (playerShots.Count < maximumPlayerShots)
            {
                playerShots.Add(new Shot(new Point(playerShip.Location.X + playerShip.Area.Width / 2, playerShip.Location.Y), Direction.Up, boundries));
            }
        }
        public void InvadersFire()
        {
            if (invaderShots.Count < maxmumInvaderSohts && invaders.Count > 0)
            {
                invaderShots.Add(new Shot(invaders[random.Next(invaders.Count)].originalPoint, Direction.Down, boundries));
            }
        }
        
        public void CheckDamageForInvaders()
        {
            if (invaders.Count > 0 && playerShots.Count > 0)
            {
                for (int i = invaders.Count - 1; i >= 0; i--)
                {
                    for (int j = playerShots.Count - 1; j >= 0; j--)
                    {
                        if (invaders[i].Attcaked(playerShots[j]))
                        {
                            score += invaders[i].Score;
                            invaders.RemoveAt(i);
                            playerShots.RemoveAt(j);
                            if (invaders.Count == 0)
                                Application.Restart();
                                break;
                            if (playerShots.Count == 0)
                                break;
                        }
                    }
                }
            }
        }

        public bool CheckDamageForPlayer()
        {
            if (invaderShots.Count > 0 && playerShip.Alive)
            {
                for (int i = invaderShots.Count - 1; i >= 0; i--)
                {
                    if (playerShip.Attacked(invaderShots[i]))
                    {
                        invaderShots.RemoveAt(i);
                        if (playerShip.Relive())
                            return true;
                        else
                            return false;
                    }
                }
            }
            return true;
        }

        public void Go()
        {
            if (playerShots.Count > 0 && playerShots.Count <= maximumPlayerShots)
            {
                for (int i = playerShots.Count - 1; i >= 0; i--)
                {
                    if (!playerShots[i].Move(Direction.Up))
                    {
                        playerShots.Remove(playerShots[i]);
                    }
                }
            }

            CheckDamageForInvaders();
            InvadersFire();


            if (invaderShots.Count > 0)
            {
                for (int i = invaderShots.Count - 1; i >= 0; i--)
                {
                    if (!invaderShots[i].Move(Direction.Down))
                        invaderShots.Remove(invaderShots[i]);
                }
            }

            foreach (Invader invader in invaders)
            {
                if (invader.Location.X >= boundries.Width - invader.Area.Width && invader.Location.Y + invader.Area.Height <= boundries.Height)
                {
                    direction = Direction.Left;
                    moveDown = true;
                    break;
                }
                else if (invader.Location.X <= 0 && invader.Location.Y + invader.Area.Height <= boundries.Height)
                {
                    direction = Direction.Right;
                    moveDown = true;
                    break;
                }
                else if(invader.Location.Y + invader.Area.Height > boundries.Height)
                {
                    playerShip.GameOver();
                    break;
                }
            }
            foreach (Invader invader in invaders)
            {
                invader.Move(direction);
                if (moveDown)
                {
                    invader.Move(Direction.Down);
                }
            }
            moveDown = false;
        }
    }
}
