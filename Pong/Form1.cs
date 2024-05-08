using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pong
{
    public partial class PongLite : Form
    {
        Pen whitePen = new Pen(Color.White, 2);
        Rectangle player1 = new Rectangle(55, 180, 50, 50);
        Rectangle player2 = new Rectangle(595, 180, 50, 50);
        Rectangle goal1 = new Rectangle(0, 150, 45, 100);
        Rectangle goal2 = new Rectangle(655, 150, 45, 100);
        Rectangle middle = new Rectangle(345, 0, 5, 400);
        Rectangle ball = new Rectangle(335, 195, 20, 20);
        Rectangle ballRec = new Rectangle(335, 200, 5, 10);
        Rectangle ballRec2 = new Rectangle(350, 200, 5, 10);
        Rectangle ballRec3 = new Rectangle(340, 195, 10, 5);
        Rectangle ballRec4 = new Rectangle(340, 210, 10, 5);

        SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.Frying_Pan_Impact);
        SoundPlayer soundPlayer2 = new SoundPlayer(Properties.Resources.Hockey_Stick_Slap);
        SoundPlayer soundPlayer3 = new SoundPlayer(Properties.Resources.Small_Crowd_Applause);

        int player1Score = 0;
        int player2Score = 0;

        int direction = 0;

        int playerSpeed = 8;
        float ballXSpeed;
        float ballYSpeed;

        bool wPressed = false;
        bool aPressed = false;
        bool sPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;
        bool downPressed = false;

        SolidBrush redBrush = new SolidBrush(Color.OrangeRed);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        public PongLite()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {

            if (ball.Y <= 0 || ball.Y >= this.Height - ball.Height)
            {
                ballYSpeed = -ballYSpeed;
            }
            else if (ball.X <= 10 || ball.X >= this.Width)
            {
                ballXSpeed = -ballXSpeed;
            }
            else if (ball.X > 680 || ball.X < 15 || ball.Y < 20 || ball.Y > 370)
            {
                ball.X = 335;
                ball.Y = 195;
                ballRec.X = 335;
                ballRec.Y = 200;
                ballRec2.X = 350;
                ballRec2.Y = 200;
                ballRec3.X = 340;
                ballRec3.Y = 195;
                ballRec4.X = 340;
                ballRec4.Y = 210;
            }

            if (wPressed == true && player1.Y > 0)
            {
                player1.Y = player1.Y - playerSpeed;
            }
            else if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y = player1.Y + playerSpeed;
            }
            else if (aPressed == true && player1.X > 0)
            {
                player1.X = player1.X - playerSpeed;
            }
            else if (dPressed == true && player1.X < this.Width && player1.X < middle.X - player1.Width)
            {
                player1.X = player1.X + playerSpeed;
            }

            if (upPressed == true && player2.Y > 0)
            {
                player2.Y = player2.Y - playerSpeed;
            }
            else if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y = player2.Y + playerSpeed;
            }
            else if (leftPressed == true && player2.X > 0 && !player2.IntersectsWith(middle))
            {
                player2.X = player2.X - playerSpeed;
            }
            else if (rightPressed == true && player2.X < this.Width - player2.Width)
            {
                player2.X = player2.X + playerSpeed;
            }

            if (player1.IntersectsWith(ballRec))
            {
                soundPlayer2.Play();
                ball.X = player1.X + player1.Width + 10;
                ballRec.X = ball.X;
                ballRec2.X = ball.X + 15;
                ballRec3.X = ball.X + 5;
                ballRec4.X = ball.X + 5;
                direction = 1;
            }
            else if (player1.IntersectsWith(ballRec2))
            {
                soundPlayer2.Play();
                ball.X = player1.X - 10;
                ballRec.X = ball.X + 15;
                ballRec2.X = ball.X;
                ballRec3.X = ball.X + 5;
                ballRec4.X = ball.X + 5;
                direction = 2;
            }
            else if (player1.IntersectsWith(ballRec4))
            {
                soundPlayer2.Play();
                ball.Y = player1.Y - player1.Height - 10;
                ballRec.Y = ball.Y + 5;
                ballRec2.Y = ball.Y + 5;
                ballRec3.Y = ball.Y;
                ballRec4.Y = ball.Y + 15;
                direction = 4;
            }
            else if (player1.IntersectsWith(ballRec3))
            {
                soundPlayer2.Play();
                ball.Y = player1.Y + player1.Height + 10;
                ballRec.Y = ball.Y + 5;
                ballRec2.Y = ball.Y + 5;
                ballRec3.Y = ball.Y;
                ballRec4.Y = ball.Y + 15;
                direction = 3;
            }
            else if (player2.IntersectsWith(ballRec2))
            {
                soundPlayer2.Play();
                ball.X = player2.X - 10;
                ballRec.X = ball.X;
                ballRec2.X = ball.X + 15;
                ballRec3.X = ball.X + 5;
                ballRec4.X = ball.X + 5;
                direction = 2;
            }
            else if (player2.IntersectsWith(ballRec))
            {
                soundPlayer2.Play();
                ball.X = player2.X + player2.Width + 10;
                ballRec.X = ball.X;
                ballRec2.X = ball.X + 15;
                ballRec3.X = ball.X + 5;
                ballRec4.X = ball.X + 5;
                direction = 1;
            }
            else if (player2.IntersectsWith(ballRec4))
            {
                soundPlayer2.Play();
                ball.Y = player2.Y - player2.Height - 10;
                ballRec.Y = ball.Y + 5;
                ballRec2.Y = ball.Y + 5;
                ballRec3.Y = ball.Y;
                ballRec4.Y = ball.Y + 15;
                direction = 4;
            }
            else if (player2.IntersectsWith(ballRec3))
            {
                soundPlayer2.Play();
                ball.Y = player1.Y + player1.Height + 10;
                ballRec.Y = ball.Y + 5;
                ballRec2.Y = ball.Y + 5;
                ballRec3.Y = ball.Y;
                ballRec4.Y = ball.Y + 15;
                direction = 3;
            }

            switch (direction)
            {
                case 0:
                    ball.X = ball.X;
                    ball.Y = ball.Y;
                    ballXSpeed = 15;
                    ballYSpeed = 15;
                    ballRec.X = ballRec.X;
                    ballRec2.X = ballRec2.X;
                    ballRec3.X = ballRec3.X;
                    ballRec4.X = ballRec4.X;
                    ballRec.Y = ballRec.Y;
                    ballRec2.Y = ballRec2.Y;
                    ballRec3.Y = ballRec3.Y;
                    ballRec4.Y = ballRec4.Y;
                    break;
                case 1:
                    ball.X = ball.X + (int) ballXSpeed;
                    ballRec.X = ballRec.X + (int) ballXSpeed;
                    ballRec2.X = ballRec2.X + (int) ballXSpeed;
                    ballRec3.X = ballRec3.X + (int) ballXSpeed;
                    ballRec4.X = ballRec4.X + (int) ballXSpeed;
                    break;
                case 2:
                    ball.X = ball.X - (int) ballXSpeed;
                    ballRec.X = ballRec.X - (int) ballXSpeed;
                    ballRec2.X = ballRec2.X - (int) ballXSpeed;
                    ballRec3.X = ballRec3.X - (int) ballXSpeed;
                    ballRec4.X = ballRec4.X - (int) ballXSpeed;
                    break;
                case 3:
                    ball.Y = ball.Y + (int) ballYSpeed;
                    ballRec.Y = ballRec.Y + (int) ballYSpeed;
                    ballRec2.Y = ballRec2.Y + (int) ballYSpeed;
                    ballRec3.Y = ballRec3.Y + (int) ballYSpeed;
                    ballRec4.Y = ballRec4.Y + (int) ballYSpeed;
                    break;
                case 4:
                    ball.Y = ball.Y - (int) ballYSpeed;
                    ballRec.Y = ballRec.Y - (int) ballYSpeed;
                    ballRec2.Y = ballRec2.Y - (int) ballYSpeed;
                    ballRec3.Y = ballRec3.Y - (int) ballYSpeed;
                    ballRec4.Y = ballRec4.Y - (int) ballYSpeed;
                    break;
            }

            if (direction != 0)
            {
                ballXSpeed = ballXSpeed * 0.975F;
                ballYSpeed = ballYSpeed * 0.975F;

                if (ballXSpeed < 0.2 || player1.IntersectsWith(ball) || player2.IntersectsWith(ball))
                {
                    direction = 0;
                }
            }

            if (goal2.IntersectsWith(ball))
            {
                soundPlayer.Play();
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";
                ball.X = 335;
                ball.Y = 195;
                ballRec.X = 335;
                ballRec.Y = 200;
                ballRec2.X = 350;
                ballRec2.Y = 200;
                ballRec3.X = 340;
                ballRec3.Y = 195;
                ballRec4.X = 340;
                ballRec4.Y = 210;
                direction = 0;
            }
            else if (goal1.IntersectsWith(ball))
            {
                soundPlayer.Play();
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";
                ball.X = 335;
                ball.Y = 195;
                ballRec.X = 335;
                ballRec.Y = 200;
                ballRec2.X = 350;
                ballRec2.Y = 200;
                ballRec3.X = 340;
                ballRec3.Y = 195;
                ballRec4.X = 340;
                ballRec4.Y = 210;
                direction = 0;
            }

            if (player1Score == 3)
            {
                soundPlayer3.Play();
                winLabel.Text = "P1 Wins!";
                gameTimer.Stop();
                //rematchButton.Visible = true;
            }

            if (player2Score == 3)
            {
                soundPlayer3.Play();
                winLabel.Text = "P2 Wins!";
                gameTimer.Stop();
                //rematchButton.Visible = true;
            }

            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(whiteBrush, goal1);
            e.Graphics.FillRectangle(whiteBrush, goal2);
            e.Graphics.FillRectangle(whiteBrush, middle);
            e.Graphics.FillRectangle(blueBrush, ballRec);
            e.Graphics.FillRectangle(blueBrush, ballRec2);
            e.Graphics.FillRectangle(blueBrush, ballRec3);
            e.Graphics.FillRectangle(blueBrush, ballRec4);
        }

        //private void rematchButton_Click(object sender, EventArgs e)
        //{
        //    rematchButton.Visible = false;
        //    gameTimer.Start();
        //    player1Score = 0;
        //    player2Score = 0;
        //    winLabel.Text = "";
        //    p1ScoreLabel.Text = $"{player1Score}";
        //    p2ScoreLabel.Text = $"{player2Score}";
        //}
    }
}