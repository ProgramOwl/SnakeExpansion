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
        bool is2Player = false;
        //private void ColorChange(), posible function for board update when color gets changed
        /*end*/

        SnakePlayer Player1;
        FoodManager FoodMngr;
        Random r = new Random();

        //to removed Code
        private int score = 0;

        public SnakeForm(bool twoPlayer)
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
            this.FormClosed += (s, e) => Application.RemoveMessageFilter(this);
            //Sam
            is2Player = twoPlayer;
            BuildBrush();
            Player1 = new SnakePlayer(this, 80, 0, Direction.right);
            if (is2Player)
            {
                Player2 = new SnakePlayer(this, 0, 80, Direction.down);
                Player2.SetColor(ColorSets[1, 0]);
            }
            //end
            FoodMngr = new FoodManager(GameCanvas.Width, GameCanvas.Height);
            FoodMngr.AddRandomFood(10);
            ScoreTxtBox.Text = score.ToString();
        }

        public void ToggleTimer()
        {
            GameTimer.Enabled = !GameTimer.Enabled;
        }

        public void ResetGame()
        {
            //Player1 = new SnakePlayer(this);
            //Sam
            Brush x = Player1.GetColor();
            Player1 = new SnakePlayer(this, 80, 0, Direction.right);
            Player1.SetColor(x);

            if (is2Player)
            {
                x = Player2.GetColor();
                Player2 = new SnakePlayer(this, 0, 80, Direction.down);
                Player2.SetColor(x);
            }
            //end
            FoodMngr = new FoodManager(GameCanvas.Width, GameCanvas.Height);
            FoodMngr.AddRandomFood(10);
            score = 0;
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
            //Sam
            if (is2Player)
            {

                String CollisionMessage = "";
                CollisionMessage = SnakeWithSnakeCollision();
                if (CollisionMessage != "")
                {
                    //Game Over Due to Collision
                    ToggleTimer(); // No timer visible on game-over screen
                    MessageBox.Show(CollisionMessage + "- GAME OVER"); // Display game-over message
                    ResetGame();
                }

                //else: proceed
                else
                {

                    /*
                    //wall collision
                    if (Player2.IsIntersectingRect(new Rectangle(-100, 0, 100, GameCanvas.Height)))
                        Player2.OnHitWall(Direction.left);

                    if (Player2.IsIntersectingRect(new Rectangle(0, -100, GameCanvas.Width, 100)))
                        Player2.OnHitWall(Direction.up);

                    if (Player2.IsIntersectingRect(new Rectangle(GameCanvas.Width, 0, 100, GameCanvas.Height)))
                        Player2.OnHitWall(Direction.right);

                    if (Player2.IsIntersectingRect(new Rectangle(0, GameCanvas.Height, GameCanvas.Width, 100)))
                        Player2.OnHitWall(Direction.down);

                    //TODO : Check the run of the food collision in case of miss earned points, though if killed from collision point get reset so should be fine

                    //food collision
                    List<Rectangle> SnakeRects2 = Player2.GetRects();
                    foreach (Rectangle rect in SnakeRects2)
                    {
                        if (FoodMngr.IsIntersectingRect(rect, true))
                        {
                            FoodMngr.AddRandomFood();
                            Player2.AddBodySegments(1);
                            Player2.UpdateScore(1);
                            //Score2TxtBox.Text = (Player2.get_points()).ToString();
                        }
                    }
                    */
                    CheckForWallCollision(Player2);
                    CheckForFoodCollision(Player2);
                }
            }

            CheckForWallCollision(Player1);
            CheckForFoodCollision(Player1);
            //end
        }
        //Sam

        private String SnakeWithSnakeCollision()
        {
            //collision between snakes
            List<Rectangle> SnakeBodyRects1 = Player1.GetRects();
            List<Rectangle> SnakeBodyRects2 = Player2.GetRects();
            Rectangle SnakeHead1 = SnakeBodyRects1.ElementAt(0);
            Rectangle SnakeHead2 = SnakeBodyRects2.ElementAt(0);

            SnakeBodyRects1.RemoveAt(0);
            SnakeBodyRects2.RemoveAt(0);
            //TODO : Check that the heads still have the right values 

            bool opposites = AreCounter();
            bool snakesCollided = false;
            String CollisionMessage = "";
            //TODO : End conditions maybe changed later on

            //Head Collision:
            if (opposites && (SnakeHead1 == SnakeHead2))
            {
                snakesCollided = true;
                int points1 = Player1.GetScore();
                int points2 = Player2.GetScore();
                CollisionMessage = "HEAD ON COLLISON";
                //tie
                if (points1 == points2)
                {
                    CollisionMessage += "- TIE ";
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
                        CollisionMessage += "- TIE ";
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

            return CollisionMessage;
        }
        private bool AreCounter()
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
        //TODO : need to check if these actually pass the update through to the actual player: if so the use them to reduce redundency

        private void CheckForWallCollision(SnakePlayer snakeX)
        {
            if (snakeX.IsIntersectingRect(new Rectangle(-100, 0, 100, GameCanvas.Height)))
                snakeX.OnHitWall(Direction.left);

            if (snakeX.IsIntersectingRect(new Rectangle(0, -100, GameCanvas.Width, 100)))
                snakeX.OnHitWall(Direction.up);

            if (snakeX.IsIntersectingRect(new Rectangle(GameCanvas.Width, 0, 100, GameCanvas.Height)))
                snakeX.OnHitWall(Direction.right);

            if (snakeX.IsIntersectingRect(new Rectangle(0, GameCanvas.Height, GameCanvas.Width, 100)))
                snakeX.OnHitWall(Direction.down);
        }
        private void CheckForFoodCollision(SnakePlayer snakeX)
        {
            List<Rectangle> SnakeRects = snakeX.GetRects();
            foreach (Rectangle rect in SnakeRects)
            {
                if (FoodMngr.IsIntersectingRect(rect, true))
                {
                    FoodMngr.AddRandomFood();
                    snakeX.AddBodySegments(1);
                    snakeX.UpdateScore(1);
                }
            }
            //Score2TxtBox.Text = (Player2.get_points()).ToString();
        }

        //end

        private void SetPlayerMovement(bool controlsSwapped, bool twoPlayer)
        {
            if (!twoPlayer)
            {
                if (!controlsSwapped)
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
            }
            else
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
                    //TODO : Maybe make the canvas go dark?
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
    }
}
