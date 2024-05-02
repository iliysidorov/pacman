using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pacman
{
    public partial class Form1 : Form
    {
        bool goup, godown, goright, goleft, isGameOver;
        int score, playerSpeed, redghostSpeed, yellowghostSpeed, pinkghostX, pinkghostY;
        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: goup = true; break;
                case Keys.S: godown = true; break;
                case Keys.D: goright = true; break;
                case Keys.A: goleft = true; break;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: goup = false; break;
                case Keys.S: godown = false; break;
                case Keys.D: goright = false; break;
                case Keys.A: goleft = false; break;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }
        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score"+ score;
            if(goleft == true)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.pacman_left;
            }
            if(goright == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.pacman_right;
            }
            if(godown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.pacman_down;
            }
            if(goup == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.pacman_up;
            }


            if (pacman.Left < -10)
            {
                pacman.Left = 680;
            }
            if (pacman.Left > 680)
            {
                pacman.Left = -10;
            }
            if (pacman.Top < -10)
            {
                pacman.Top = 550;
            }
            if (pacman.Top > 550)
            {
                pacman.Top = 0;

            }
            foreach (Control x in this.Controls)
            {
                if ((String)x.Tag == "coin" && x.Visible == true)
                {
                    if (pacman.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        x.Visible = false;
                    }
                }

                if ((string)x.Tag == "wall")
                {
                    if (pacman.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameOver("You Lose!");
                    }


                }
                if ((string)x.Tag == "ghost")
                {
                    if (pacman.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameOver("You Lose!");
                    }
                }
            }
            if (goup)
            {
                pacman.Top -= 11;

            }
            if (godown)
            {
                pacman.Top += 11;
            }
            if (goleft)
            {
                pacman.Left -= 11;
            }
            if (goright)
            {
                pacman.Left += 11;
            }






            //moving ghosts
            redghost.Left += redghostSpeed;
            if (redghost.Bounds.IntersectsWith(pictureBox3.Bounds) || redghost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                redghostSpeed = -redghostSpeed;
            }
            yellowghost.Left -= yellowghostSpeed;
            if (yellowghost.Bounds.IntersectsWith(pictureBox2.Bounds) || yellowghost.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                yellowghostSpeed = -yellowghostSpeed;
            }


            pinkghost.Left -= pinkghostX;
            pinkghost.Top -= pinkghostY;

            if (pinkghost.Top < 0 || pinkghost.Top > 520)
            {
                pinkghostY = -pinkghostY;
            }

            if (pinkghost.Left < 0 || pinkghost.Left > 620)
            {
                pinkghostX = -pinkghostX;
            }

            if (score == 46)
            {
                gameOver("You Win!");
            }
        }
        private void resetGame()
        {

            txtScore.Text = "Score: 0";

            score = 0;

            redghostSpeed = 6;
            yellowghostSpeed = 6;

            pinkghostX = 6;
            pinkghostY = 6;
            playerSpeed = 8;

            isGameOver = false;

            pacman.Left = 31;
            pacman.Top = 46;

            redghost.Left = 208;
            redghost.Top = 55;

            yellowghost.Left = 448;
            yellowghost.Top = 445;

            pinkghost.Left = 525;
            pinkghost.Top = 235;

            gameTimer.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }

        }
        private void gameOver(string message)
        {

            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text = "Score " + score + Environment.NewLine + message;

        }
    }
}
