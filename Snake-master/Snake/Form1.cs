using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Snake
{
    public partial class SnakeForm : Form, IMessageFilter
    {
        /*Sam*/
        //Next step in the future would be to have the game build based on the number of players passed in through a series of arrays
        //TODO : 
        //-add spawn location system, 
        //-Have a Player2 score display
        //-test that current code works (would need to add p2 controls though)
        //^use to build Unit Tests (-check that Snake head is correctly Identified, -check that the rectangles are accuratly being compared)

        Brush[,] ColorSets = new Brush[2, 3];
        private void BuildBrush()
        {
            ColorSets[0, 0] = Brushes.Black;
            ColorSets[0, 1] = Brushes.Lavender;
            ColorSets[0, 2] = Brushes.MistyRose;

            ColorSets[1, 0] = Brushes.CornflowerBlue;
            ColorSets[1, 1] = Brushes.PeachPuff;
            ColorSets[1, 2] = Brushes.LawnGreen;
        }
        SnakePlayer Player2;
        public bool is2Player = false;
        Startscreen mainmenu;
        //private void ColorChange(), posible function for board update when color gets changed
        /*end*/

        bool controlsSwapped = false;
        bool gridVisible = false;
        SnakePlayer Player1;
        FoodManager FoodMngr;
        Random r = new Random();
        private void PauseTimer_Tick(object sender, EventArgs e)
        {
            if (Input.IsKeyDown(Keys.P))
            {
                ToggleTimer();
            }
        }
        public SnakeForm(Startscreen menu, bool twoPlayer)
        {
            PauseTimer = new Timer();
            InitializeComponent();
            PauseTimer.Tick += PauseTimer_Tick;
            PauseTimer.Start();
            Application.AddMessageFilter(this);
            this.FormClosed += (s, e) => Application.RemoveMessageFilter(this);
            //Sam
            BuildBrush();
            mainmenu = menu;
            is2Player = twoPlayer;
            skin1comboBox.SelectedIndex = 0;
            skin2comboBox.SelectedIndex = 0;

            SnakeSetup();
            DisplaySetup();
            //end
            FoodMngr = new FoodManager(GameCanvas.Width, GameCanvas.Height);
            FoodMngr.AddRandomFood(10);

            this.FormClosing += SnakeForm1_FormClosing;
        }

        private void SnakeSetup()
        {
            //Brush x = Player1.GetColor();
            //Player1 = new SnakePlayer(this, 80, 20, Direction.right);
            //Player1.SetColor(x);
            //ScoreTxtBox.Text = Player1.GetScore().ToString();

            //if (is2Player)
            //{
            //    x = Player2.GetColor();
            //    Player2 = new SnakePlayer(this, 20, 80, Direction.down);
            //    Player2.SetColor(x);
            //    //Score2TxtBox.Text = (Player2.get_points()).ToString();
            //}

            Player1 = new SnakePlayer(this, 80, 20, Direction.right);
            ScoreTxtBox.Text = Player1.GetScore().ToString();
            int col = skin1comboBox.SelectedIndex;
            Player1.SetColor(ColorSets[0, col]);

            if (is2Player)
            {
                Player2 = new SnakePlayer(this, 20, 80, Direction.down);
                Score2TxtBox.Text = Player2.GetScore().ToString();
                int col2 = skin2comboBox.SelectedIndex;
                Player2.SetColor(ColorSets[1, col2]);
            }
        }
       
        //create a visual set up based on number of players
        private void DisplaySetup()
        {
            if (!is2Player)
            {
                Player2Label.Visible = false;
                Score2Label.Visible = false;
                Score2TxtBox.Visible = false;
                skin2label.Visible = false;
                skin2comboBox.Visible = false;
            }

        }
        //create an initilize snakes based on number of players
        //end

        private void SnakeForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to return to the main menu?", "Snake Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                mainmenu.Show();
            }
            else
            {
                mainmenu.subExit();
            }
        }

        public void ToggleTimer()
        {
            GameTimer.Enabled = !GameTimer.Enabled;
        }

        public void GameHasEnded(String CollisionMessage)
        {
            //Game Over Due to Collision
            ToggleTimer(); // No timer visible on game-over screen
            PauseTimer.Stop();
            if(MessageBox.Show(CollisionMessage + " - GAME OVER \nDo you want to play again?", "Snake Game", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetGame();
            }
            else
            {
                this.Close();
            }
            //MessageBox.Show(CollisionMessage + "- GAME OVER"); // Display game-over message
            //ResetGame();
        }

        public void ResetGame()
        {
            //Sam           
            SnakeSetup();
            //end
            FoodMngr = new FoodManager(GameCanvas.Width, GameCanvas.Height);
            FoodMngr.AddRandomFood(10);
            DisplaySetup();
        }

        public bool PreFilterMessage(ref Message msg)
        {
            if (msg.Msg == 0x0101) //KeyUp
                Input.SetKey((Keys)msg.WParam, false);
            return false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (msg.Msg == 0x100) //KeyDown
                Input.SetKey((Keys)msg.WParam, true);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GameCanvas_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics canvas = e.Graphics;
            if (gridVisible)
            {
                Grid_Paint(canvas, 100);
            }
            Player1.Draw(canvas);
            //Sam
            if (is2Player)
            {
                Player2.Draw(canvas);
            }
            //end
            FoodMngr.Draw(canvas);
            
        }

        private void CheckForCollisions()
        {
            //Sam
            if (is2Player)
            {
                if (SnakeWithSnakeCollision())
                {
                    //GameHasEnded(CollisionMessage);
                }
                else
                {
                    bool col1 = CheckForWallCollision(Player1);
                    bool col2 = CheckForWallCollision(Player2);
                    if (col1 || col2)
                    {
                        String mes = (col1 == col2 ? "- Tie" : (col1 ? "- Player2 Wins " : "- Player1 Wins"));
                        GameHasEnded("Hit wall " + mes);
                    }

                    if (CheckForFoodCollision(Player1))
                        ScoreTxtBox.Text = Player1.GetScore().ToString();
                    if (CheckForFoodCollision(Player2))
                        Score2TxtBox.Text = (Player2.GetScore()).ToString();
                }
            }
            else
            {
                if (CheckForWallCollision(Player1))
                    GameHasEnded("Hit wall ");
                else if (CheckForFoodCollision(Player1))
                    ScoreTxtBox.Text = Player1.GetScore().ToString();
            }
            //end
        }

        //Sam: Collision Checking Methods
        /*
            if (Player1.IsIntersectingRect(new Rectangle(-100, 0, 100, GameCanvas.Height)))
                Player1.OnHitWall(Direction.left);

            if (Player1.IsIntersectingRect(new Rectangle(0, -100, GameCanvas.Width, 100)))
                Player1.OnHitWall(Direction.up);

            if (Player1.IsIntersectingRect(new Rectangle(GameCanvas.Width, 0, 100, GameCanvas.Height)))
                Player1.OnHitWall(Direction.right);

            if (Player1.IsIntersectingRect(new Rectangle(0, GameCanvas.Height, GameCanvas.Width, 100)))
                Player1.OnHitWall(Direction.down);

            //Is hitting food
            List<Rectangle> SnakeRects = Player1.GetRects();
            foreach(Rectangle rect in SnakeRects)
            {
                if(FoodMngr.IsIntersectingRect(rect,true))
                {
                    FoodMngr.AddRandomFood();
                    Player1.AddBodySegments(1);
                    score++;
                    ScoreTxtBox.Text = score.ToString();
                }
            }*/

        private bool SnakeWithSnakeCollision()
        {
            //collision between snakes
            List<Rectangle> SnakeBodyRects1 = Player1.GetRects();
            List<Rectangle> SnakeBodyRects2 = Player2.GetRects();
            Rectangle SnakeHead1 = SnakeBodyRects1.ElementAt(0);
            Rectangle SnakeHead2 = SnakeBodyRects2.ElementAt(0);

            //SnakeBodyRects1.RemoveAt(0);
            //SnakeBodyRects2.RemoveAt(0);
            //TODO : Check that the heads still have the right values 

            bool opposites = AreHeadbutting();
            bool snakesCollided = false;
            String CollisionMessage = "";
            //TODO : End conditions maybe changed later on

            //Head Collision:
            if (opposites && (SnakeHead1 == SnakeHead2))
            {
                snakesCollided = true;
                int points1 = Player1.GetScore();
                int points2 = Player2.GetScore();
                CollisionMessage = "Head On Collision ";
                //tie
                if (points1 == points2)
                {
                    CollisionMessage += "- Tie ";
                }
                //p1 is bigger
                else if (points1 >= points2)
                {
                    CollisionMessage += "- Player1 Wins ";
                }
                //p2 is bigger
                else
                {
                    CollisionMessage += "- Player2 Wins ";
                }
            }
            //Side Collision:
            else
            {
                bool player2Crash = false;
                bool player1Crash = false;
                //check if player2 crashed into player1
                foreach (Rectangle rect in SnakeBodyRects1)
                {
                    if (SnakeHead2 == rect)
                    {
                        snakesCollided = true;
                        player2Crash = true;
                    }
                }
                //check if player1 crashed into player2
                foreach (Rectangle rect in SnakeBodyRects2)
                {
                    if (SnakeHead1 == rect)
                    {
                        snakesCollided = true;
                        player1Crash = true;
                    }
                }

                if (snakesCollided)
                {
                    CollisionMessage = "Ran Into Other Player ";

                    if (player1Crash && player2Crash)
                    {
                        CollisionMessage += "- Tie ";
                    }
                    //p1 is bigger
                    else if (player2Crash)
                    {
                        CollisionMessage += "- Player1 Wins ";
                    }
                    //p2 is bigger
                    else if (player1Crash)
                    {
                        CollisionMessage += "- Player2 Wins ";
                    }
                }
            }

            //send message of game end
            if (snakesCollided)
            {
                GameHasEnded(CollisionMessage);
            }

            //return CollisionMessage;
            return snakesCollided;
        }
        private bool AreHeadbutting()
        {
            String dir1 = Player1.GetCurrentDirection();
            String dir2 = Player2.GetCurrentDirection();
            bool oppositeDirections = false;

            if (dir1 == "left" && dir2 == "right")
                oppositeDirections = true;

            if (dir1 == "right" && dir2 == "left")
                oppositeDirections = true;

            if (dir1 == "up" && dir2 == "down")
                oppositeDirections = true;

            if (dir1 == "down" && dir2 == "up")
                oppositeDirections = true;

            return oppositeDirections;
        }
        private bool CheckForWallCollision(SnakePlayer snakeX)
        {
            if (snakeX.IsIntersectingRect(new Rectangle(-100, 0, 100, GameCanvas.Height)))
                return true;
            //snakeX.OnHitWall(Direction.left);

            if (snakeX.IsIntersectingRect(new Rectangle(0, -100, GameCanvas.Width, 100)))
                return true;
            //snakeX.OnHitWall(Direction.up);

            if (snakeX.IsIntersectingRect(new Rectangle(GameCanvas.Width, 0, 100, GameCanvas.Height)))
                return true;
            //snakeX.OnHitWall(Direction.right);

            if (snakeX.IsIntersectingRect(new Rectangle(0, GameCanvas.Height, GameCanvas.Width, 100)))
                return true;
            //snakeX.OnHitWall(Direction.down);
            return false;
        }
        private bool CheckForFoodCollision(SnakePlayer snakeX)
        {
            bool hitFood = false;
            List<Rectangle> SnakeRects = snakeX.GetRects();
            foreach (Rectangle rect in SnakeRects)
            {
                if (FoodMngr.IsIntersectingRect(rect, true))
                {
                    FoodMngr.AddRandomFood();
                    snakeX.AddBodySegments(1);
                    snakeX.UpdateScore(1);
                    hitFood = true;
                }
            }
            return hitFood;
        }

        //end

        //controls
        private void Ctrl_Toggle_Click(object sender, EventArgs e)
        {
            controlsSwapped = !controlsSwapped;
            if (controlsSwapped)
                Ctrl_Toggle.Text = "Controls: WASD";
            else if(!controlsSwapped)
                Ctrl_Toggle.Text = "Controls: Arrows";
            else
                Ctrl_Toggle.Text = "Controls: null";
        }

        private void SetPlayerMovement()
        {
            
            if (!twoPlayer)
            {
                if (Input.IsKeyDown(Keys.Left))
                {
                    Player1.SetDirection(Direction.left);
                }
                else if (Input.IsKeyDown(Keys.Right))
                {
                    Player1.SetDirection(Direction.right);
                }
                else if (Input.IsKeyDown(Keys.Up))
                {
                    Player1.SetDirection(Direction.up);
                }
                else if (Input.IsKeyDown(Keys.Down))
                {
                    Player1.SetDirection(Direction.down);
                }
            }
            else
            {
                if (Input.IsKeyDown(Keys.A))
                {
                    Player1.SetDirection(Direction.left);
                }
                else if (Input.IsKeyDown(Keys.D))
                {
                    Player1.SetDirection(Direction.right);
                }
                else if (Input.IsKeyDown(Keys.W))
                {
                    Player1.SetDirection(Direction.up);
                }
                else if (Input.IsKeyDown(Keys.S))
                {
                    Player1.SetDirection(Direction.down);
                }
            }

            if (is2Player)
            {
                if (!controlsSwapped)
                {
                    if (Input.IsKeyDown(Keys.A))
                    {
                        Player2.SetDirection(Direction.left);
                    }
                    else if (Input.IsKeyDown(Keys.D))
                    {
                        Player2.SetDirection(Direction.right);
                    }
                    else if (Input.IsKeyDown(Keys.W))
                    {
                        Player2.SetDirection(Direction.up);
                    }
                    else if (Input.IsKeyDown(Keys.S))
                    {
                        Player2.SetDirection(Direction.down);
                    }
                }
                else
                {
                    if (Input.IsKeyDown(Keys.Left))
                    {
                        Player2.SetDirection(Direction.left);
                    }
                    else if (Input.IsKeyDown(Keys.Right))
                    {
                        Player2.SetDirection(Direction.right);
                    }
                    else if (Input.IsKeyDown(Keys.Up))
                    {
                        Player2.SetDirection(Direction.up);
                    }
                    else if (Input.IsKeyDown(Keys.Down))
                    {
                        Player2.SetDirection(Direction.down);
                    }
                }
                Player2.MovePlayer();
            }
            Player1.MovePlayer();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            
            SetPlayerMovement(false, is2Player);
            CheckForCollisions();
            GameCanvas.Invalidate();
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {
            ToggleTimer();
        }

        private void DareBtn_Click(object sender, EventArgs e)
        {
            //TODO : maybe remove or make invalid after a set number of press or while game is paused?
            int index = r.Next(4);
            switch (index)
            {
                case 0:
                    MessageBox.Show("How dare you listen");
                    break;
                case 1:
                    MessageBox.Show("This is a dark path you are on");
                    //TODO Bob : Maybe make the canvas go dark?
                    //apply a background to the grid (I suggest a bool for state( if bright or dark)
                    //when they hit this make the background black, the grid lines white
                    //if the hit this again then make it back to light (change the message in the message box accordingly
                    break;
                case 2:
                    MessageBox.Show("I knew you wouldn't listen");
                    break;
                case 3:
                    MessageBox.Show("Have some food :)");
                    FoodMngr.AddRandomFood(20);
                    GameCanvas.Invalidate();
                    break;
                default:
                    break;
            }
        }

        //Grid
        private void ToggleGrid_CheckedChanged(object sender, EventArgs e)
        {
            gridVisible = !gridVisible;
            GameCanvas.Invalidate();
        }
        private void Grid_Paint(Graphics canv, int cells)
        {
            int cellSize = 20;
            Pen p = new Pen(Color.Black);

            for (int y = 0; y < cells; ++y)
            {
                canv.DrawLine(p, 0, y * cellSize, cells * cellSize, y * cellSize);
            }

            for (int x = 0; x < cells; ++x)
            {
                canv.DrawLine(p, x * cellSize, 0, x * cellSize, cells * cellSize);
            }
        }

        private void skin1comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int col = skin1comboBox.SelectedIndex;
            if(Player1 != null)
                Player1.SetColor(ColorSets[0, col]);
            GameCanvas.Invalidate();
        }

        private void skin2comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int col = skin2comboBox.SelectedIndex;
            if (Player2 != null)
                Player2.SetColor(ColorSets[1, col]);
            GameCanvas.Invalidate();
        }
    }
}
