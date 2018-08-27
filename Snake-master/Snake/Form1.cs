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
using System.Collections;
using System.Reflection;

namespace Snake
{
    public partial class SnakeForm : Form, IMessageFilter
    {
        /*Sam*/
        //Next step in the future would be to have the game build based on the number of players passed in through a series of arrays
        //TODO : 
        //-test that current code works (would need to add p2 controls though)
        //^use to build Unit Tests (-check that Snake head is correctly Identified, -check that the rectangles are accuratly being compared)
        //re enable and paause game upon restart
        //test the cancle

        //
        // Color Selections setup
        //
        private void BuildBrush()
        {
            ArrayList ColorList = new ArrayList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static |
                                          BindingFlags.DeclaredOnly | BindingFlags.Public);

            
            int numOfPayers = (is2Player ? 2 : 1);
            int count = (int)((propInfoList.Length-1) / numOfPayers);
            int ind = 1;

            for (int i = 0; i < count; i++)
            {
                this.skin1comboBox.Items.Add(propInfoList[ind].Name);
                ind++;
                if (is2Player)
                {
                    this.skin2comboBox.Items.Add(propInfoList[ind].Name);
                    ind++;
                }
            }
            
            skin1comboBox.SelectedIndex = 0;
            if (is2Player)
            {
                skin2comboBox.SelectedIndex = 0;
            }
        }
        //
        // Color Selections display styling
        //
        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Brush b = new SolidBrush(Color.FromName(n));

                Font f = new Font("Cambria", 10, FontStyle.Regular);
                g.DrawString(n, f, Brushes.Black, rect.X + 22, rect.Top);
                g.FillRectangle(b, rect.X, rect.Y, 20, rect.Height);
                //g.DrawRectangle(penB, rect.X, rect.Y, rect.Width-1, rect.Height - 1);
            }
        }

        //parent access for termination of program and return to menu
        Startscreen mainmenu;
        //black pen used for drawing the borders in the comboBox Items
        Pen penB = new Pen(Brushes.Black); 
        //game states
        bool is2Player = false;
        bool controlsSwapped = false;
        bool gridVisible = false;
        bool gameHasEnded = false;
        bool gameInAction = false;
        bool gridInversed = false;
        //players
        SnakePlayer Player1;
        SnakePlayer Player2;
        //food
        FoodManager FoodMngr;
        Random r = new Random();
        //Timer PauseTimer = new Timer();
        
        public SnakeForm(Startscreen menu, bool twoPlayer)
        {
            InitializeComponent();

            //xxx
            //PauseTimer = new Timer();
            //PauseTimer.Tick += PauseTimer_Tick;
            //PauseTimer.Start();

            Application.AddMessageFilter(this);
            this.FormClosed += (s, e) => Application.RemoveMessageFilter(this);
           

            //set link to form's parent
            mainmenu = menu;
            //update the game's player state
            is2Player = twoPlayer;
            //set the name of the current Game window
            this.Text = "Welcome to Snake: " + (is2Player?"Two":"Single") + " Player Mode";
            //set the brush visuals
            BuildBrush();
            //Build Players and update the visuals
            SnakeSetup();
            DisplaySetup();
            
            //Food initialization and execution
            FoodMngr = new FoodManager(GameCanvas.Width, GameCanvas.Height);
            FoodMngr.AddRandomFood(10);

            //apply exit message
            this.FormClosing += SnakeForm1_FormClosing;

            //initial start
            CheckForCollisions();

            gameInAction = false;
            ToggleGame();

            //PauseTimer = new Timer();
            //PauseTimer.Tick += PauseTimer_Tick;
            //PauseTimer.Start();
            //GameTimer = new Timer();
            //GameTimer.Tick += GameTimer_Tick;
            GameTimer.Enabled = true;

        }

        private void SnakeSetup()
        {
            Player1 = new SnakePlayer(this, 80, 20, Direction.right);
            ScoreTxtBox.Text = Player1.GetScore().ToString();
            setSkin(skin1comboBox, Player1);

            if (is2Player)
            {
                Player2 = new SnakePlayer(this, 20, 80, Direction.down);
                Score2TxtBox.Text = Player2.GetScore().ToString();
                setSkin(skin2comboBox, Player2);
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
            //make current game pause
            // TODO
            //GameTimer.Enabled = false;
            //send message box
            
            gameInAction = false;
            ToggleGame();

            //PauseTimer.Enabled = false;

            DialogResult result = MessageBox.Show("Do you want to return to the main menu?", "Snake Game", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                mainmenu.Show();
            }
            else if (result == DialogResult.Cancel)
            {
                //remove message and return to game
                e.Cancel = true;
                //GameTimer.Enabled = true;
                if (gameHasEnded)
                {
                    ResetGame();
                }
            }
            else if (result == DialogResult.No)
            {
                mainmenu.subExit();
            }
        }

        public void GameHasEnded(String CollisionMessage)
        {
            gameHasEnded = true;
            //Game Over Due to Collision
            //GameTimer.Enabled = false;
            gameInAction = false;
            ToggleGame();
            //ToggleTimer(); // No timer visible on game-over screen
            //xxx
            //PauseTimer.Stop();
            //PauseTimer.Enabled = false;

            if (MessageBox.Show(CollisionMessage + " - GAME OVER \nDo you want to play again?", "Snake Game", 
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
            //PauseTimer.Enabled = true;
            gameHasEnded = false;
            gameInAction = false;
            ToggleGame();
            SnakeSetup();

            FoodMngr = new FoodManager(GameCanvas.Width, GameCanvas.Height);
            FoodMngr.AddRandomFood(10);

            DisplaySetup();
            //GameTimer.Enabled = true;
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

            if (is2Player)
            {
                Player2.Draw(canvas);
            }
            FoodMngr.Draw(canvas);            
        }

        private void CheckForCollisions()
        {
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
        }

        //Collision Check Methods
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

        //int ticks = 0;
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //ticks = (ticks == 0 ? 0 : (ticks - 1));
            //if (Input.IsKeyDown(Keys.P))
            //{
            //    ToggleGame();
            //}

            if (gameInAction)
            {
                SetPlayerMovement();
                CheckForCollisions();
                GameCanvas.Invalidate();
            }
            //else if (Input.IsKeyDown(Keys.P) && !gameHasEnded)
            //{
            //    gameInAction = !gameInAction;
            //    ToggleGame();
            //    //ToggleTimer();
            //}
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {
            //if (!Input.IsKeyDown(Keys.P))
            //{
            gameInAction = !gameInAction;
            ToggleGame();
            //}
            //else
            //{
                //ticks = (ticks == 0 ? 0 : (ticks - 1));
            //}
            //ToggleGame();
        }
        public void ToggleGame()
        {
            //if (ticks == 0)
            //{
            //    ticks = 4;
                //gameInAction = !gameInAction;
                if (gameInAction)
                {
                //GameTimer.Start();
                //PauseTimer.Enabled = false;
                //GameTimer.Enabled = true;
                    this.Ctrl_Toggle.Enabled = false;
                    this.skin1comboBox.Enabled = false;
                    this.skin2comboBox.Enabled = false;
                    this.ToggleGrid.Enabled = false;
                    this.DareBtn.Enabled = false;
                    this.Start_Btn.Text = "Pause";
                }
                else
                {
                    //GameTimer.Stop();
                    //PauseTimer.Enabled = true;
                    //GameTimer.Enabled = false;
                    this.Ctrl_Toggle.Enabled = true;
                    this.skin1comboBox.Enabled = true;
                    this.skin2comboBox.Enabled = true;
                    this.ToggleGrid.Enabled = true;
                    this.DareBtn.Enabled = true;
                    this.Start_Btn.Text = "Start";
                }
            //}
            //else
            //{
            //    ticks = (ticks == 1 ? 0 : (ticks - 1));
            //}

            //System.Threading.Thread.Sleep(2000);
        }

       
        private void PauseTimer_Tick(object sender, EventArgs e)
        {
            //if (Input.IsKeyDown(Keys.P) && !gameInAction) 
            //{
            //    gameInAction = true;
            //    ToggleGame();
            //    //ToggleTimer();
            //}
            //System.Threading.Thread.Sleep(1000);
            //else if (Input.IsKeyDown(Keys.O) && !gameHasEnded)
            //{
            //    gameInAction = true;
            //    //ToggleGame();
            //    //ToggleTimer();
            //}
            //else
            //{
            //    //ToggleGame();
            //}
        }

        private void DareBtn_Click(object sender, EventArgs e)
        {
            int index = r.Next(4);
            switch (index)
            {
                case 0:
                    MessageBox.Show("How dare you listen");
                    break;
                case 1:
                    if (!gridInversed)
                    {
                        MessageBox.Show("This is a dark path you are on");
                    }
                    else
                    {
                        MessageBox.Show("Welcome back to the light.");
                    }
                    gridInversed = !gridInversed;
                    if (GameCanvas.BackColor == Color.FromArgb(224, 224, 224))
                    {
                        GameCanvas.BackColor = Color.Black;

                    }
                    else if (GameCanvas.BackColor == Color.Black)
                    {
                        GameCanvas.BackColor = Color.FromArgb(224, 224, 224);
                    }
                    GameCanvas.Invalidate();
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
            Pen p = (gridInversed ? new Pen(Color.White) : new Pen(Color.Black));
            int cellSize = 20;

            for (int y = 0; y < cells; ++y)
            {
                canv.DrawLine(p, 0, y * cellSize, cells * cellSize, y * cellSize);
            }

            for (int x = 0; x < cells; ++x)
            {
                canv.DrawLine(p, x * cellSize, 0, x * cellSize, cells * cellSize);
            }
        }

        //Snake skin setting
        private void skin1comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSkin((ComboBox)sender, Player1);
        }
        private void skin2comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSkin((ComboBox)sender, Player2);
        }

        //shouldn't get out of range but easy fix by adding a range max limit
        private void setSkin(ComboBox sender, SnakePlayer sp)
        {
            if (sp != null)
            {
                int col = sender.SelectedIndex;
                string n = sender.Items[(col>=0?col:0)].ToString();
                Brush b = new SolidBrush(Color.FromName(n));
                sp.SetColor(b);
                GameCanvas.Invalidate();
            }
        }
    }
}
