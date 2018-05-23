using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invader
{
    public partial class Form1 : Form
    {
        List<Keys> keyPressed = new List<Keys>();
        Game game;
        bool gameOver = false;


        public Form1()
        {
            InitializeComponent();
            game = new Game(ClientRectangle);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();

            if(gameOver)
                if(e.KeyCode == Keys.S)
                {
                    //code to reset the game and restart the timers
                    return;
                }

            if (e.KeyCode == Keys.Space)
                game.FireShot();
            if (keyPressed.Contains(e.KeyCode))
                keyPressed.Remove(e.KeyCode);
            keyPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keyPressed.Contains(e.KeyCode))
                keyPressed.Remove(e.KeyCode);
        }

        private void gameTImer_Tick(object sender, EventArgs e)
        {
            game.Go();
            if (!game.CheckDamageForPlayer())
            {
                gameTimer.Enabled = false;
            }
            foreach (Keys key in keyPressed)
            {
                if (key == Keys.Left)
                {
                    game.MovePlayer(Direction.Left);
                    return;
                }
                else if (key == Keys.Right)
                {
                    game.MovePlayer(Direction.Right);
                    return;
                }
                else if (key == Keys.Space && !keyPressed.Contains(Keys.Space))
                {
                    game.FireShot();
                }
            }
        }

        int animationCell = 0;
        int frame = 0;
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            game.Twinkle();
            frame++;
            if (frame >= 6)
                frame = 0;
            switch (frame)
            {
                case 0: animationCell = 0; break;
                case 1: animationCell = 1; break;
                case 2: animationCell = 2; break;
                case 3: animationCell = 3; break;
                case 4: animationCell = 2; break;
                case 5: animationCell = 1; break;
                default: animationCell = 0; break;
            }

        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.Draw(g, animationCell);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
