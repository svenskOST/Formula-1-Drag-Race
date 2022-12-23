using Timer = System.Threading.Timer;

namespace Formula_1_Drag_Race
{
    public partial class GameForm : Form
    {
        double player1Speed = 0;
        double player2Speed = 0;
        readonly double maxSpeed = 100;
        readonly double maxReverse = -40;

        bool wDown;
        bool sDown;
        bool upDown;
        bool downDown;
        bool gameFinished;

        Graphics? graphics;

        public GameForm()
        {
            InitializeComponent();
        }

        private void RefreshGame(object sender, EventArgs e)
        {
            if (player1.Right >= finishLine1.Left && gameFinished == false)
            {
                label1.Text = "Player 1 wins!";
                gameFinished = true;
                player1Speed = 0;
                player2Speed = 0;
                //timer1.Stop();
                //freeroam or restart
            }

            if (player2.Right >= finishLine1.Left && gameFinished == false)
            {
                label1.Text = "Player 2 wins!";
                gameFinished = true;
                player1Speed = 0;
                player2Speed = 0;
                //timer1.Stop();
                //freeroam or restart
            }

            if (Focused)
            {
                if (wDown == true)
                {
                    if (player1Speed < maxSpeed)
                    {
                        player1Speed += 0.3;
                    }
                }
                else if (sDown == true)
                {
                    if (player1Speed > maxReverse)
                    {
                        player1Speed -= 0.2;
                    }
                }

                if (upDown == true)
                {
                    if (player2Speed < maxSpeed)
                    {
                        player2Speed += 0.3;
                    }
                }
                else if (downDown == true)
                {
                    if (player2Speed > maxReverse)
                    {
                        player2Speed -= 0.2;
                    }
                }
            }

            if (player1Speed > 0 && wDown == false)
            {
                player1Speed -= 0.1;
            }
            else if (player1Speed < 0 && sDown == false)
            {
                player1Speed += 0.1;
            }

            if (player2Speed > 0 && upDown == false)
            {
                player2Speed -= 0.1;
            }
            else if (player2Speed < 0 && downDown == false)
            {
                player2Speed += 0.1;
            }

            if (player1.Left >= 1280)
            {
                player1.Left = -200;
            }
            else if (player1.Right <= 0)
            {
                player1.Left = 1280;
            }

            if (player2.Left >= 1280)
            {
                player2.Left = -200;
            }
            else if (player2.Right <= 0)
            {
                player2.Left = 1280;
            }

            player1.Left += (int)Math.Round(player1Speed);
            player2.Left += (int)Math.Round(player2Speed);
        }

        private void IsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                wDown = true;
            }
            else if (e.KeyCode == Keys.S)
            {
                sDown = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                upDown = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                downDown = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                LightsOut();
            }
        }

        private void IsKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                wDown = false;
            }
            else if (e.KeyCode == Keys.S)
            {
                sDown = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                upDown = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                downDown = false;
            }
        }

        private void LightsOut()
        {
            label2.Visible = false;

            graphics = CreateGraphics();

            Rectangle container = new(377, 270, 525, 100);
            graphics.FillRectangle(Brushes.Black, container);

            Rectangle[] light = new Rectangle[5];

            for (int i = 0; i < light.Length; i++)
            {
                light[i] = new(398 + i * 101, 280, 80, 80);
                graphics.FillEllipse(Brushes.DarkGray, light[i]);
            }

            for (int i = 0; i < light.Length; i++)
            {
                Thread.Sleep(1000);
                graphics.FillEllipse(Brushes.Red, light[i]);
            }
            Random random = new();
            int rand = random.Next(200, 4000);
            Thread.Sleep(rand);

            for (int i = 0; i < light.Length; i++)
            {
                graphics.FillEllipse(Brushes.DarkGray, light[i]);
            }
            timer1.Start();
            Timer timer = new(HideLights, null, 1000, Timeout.Infinite);
        }

        private void HideLights(object? state)
        {
            Invalidate();
            graphics?.Dispose();
        }
    }
}

//l�gga till jumpstart och cheating (om man backar)
//g�ra f�+nstret och contenten responsiv/anpassningsbar
//n�r n�gon vunnit ska det komma upp ett val s� man kan reset game eller freeroam (med en paus ikon)
//g�ra meny som l�ter en g� direkt till freeroam eller dragrace
//l�gga till UI som visar speed, distance och reaction time f�r player 1 och 2
//g�ra om player variables till objekt (player1) med egenskaper (maxspeed) och metoder (Drive eller accelerate osv) 
//fixa s� bilarnas movement blir smooth, just nu hackigt f�r att jag tvingas avrunda utr�kningen av positionerna vid varje tick - s� de hoppar tillbaka ibland