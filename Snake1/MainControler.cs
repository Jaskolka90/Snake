using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake1
{
    public class MainControler
    {
        private MainView mainView;
        private FruitControler fruitControler;
        private Snake snake;      
        private Timer mainTimer;
        private Random random = new Random();
        private int size = 40;
        private int amountOfLife = 3;
        private int points = 0;
        private int time = 0;
        private bool pause = true;
        private bool isGameOver = false;
        private bool CanEatItself;
        private bool CanGothroughWalls;

        public MainControler(MainView mainView)
        {
            this.mainView = mainView;
            this.mainView.Init(this);
            Size panelSize = this.mainView.GetPanelSize();

            this.mainTimer = new Timer();
            this.mainTimer.Interval = 500;
            this.mainTimer.Tick += MainTimer_Tick;

            NewGame();
            fruitControler = new FruitControler(size, panelSize, this);
            fruitControler.InitFruit(3);
        }

        private void NewGame()
        {
            if (isGameOver)
            {
                amountOfLife = 3;
                points = 0;
            }
            CanEatItself = false;
            CanGothroughWalls = false;
            mainView.SetLives(amountOfLife);
            mainView.SetPoints(points);
            SetPause( true );
            Size panelSize = this.mainView.GetPanelSize();
            this.snake = new Snake(new Point(panelSize.Width / 2, panelSize.Height / 2), size, panelSize);
            Time(0);
            this.mainView.SetCanGothroughWalls(CanGothroughWalls);
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            this.snake.Move();
            this.mainView.RefreshPanel();
            ProcessColisions();
        }

        public void ProcessColisions()
        {
            if (CanGothroughWalls)
            {
                time++;
                Time(time);
                if (time >= 20)
                {
                    CanGothroughWalls = false;
                    this.mainView.SetCanGothroughWalls(CanGothroughWalls);
                    this.snake.SetCanGothroughWalls(CanGothroughWalls);
                    time = 0;
                }
            }
            if (CanEatItself)
            {
                time++;
                Time(time);
                if (time >= 20)
                {
                    CanEatItself = false;
                    this.snake.SetCanEatItself(CanEatItself);
                    time = 0;
                }
            }

            if (Colizions.IsSnakeColidetWithItSelf(snake))
            {
                if (CanEatItself == false)
                {
                    SnakeBeatUp();
                }
            }
            if (Colizions.IsSnakeColidedWithBoards(snake, mainView.GetPanelSize()))
            {
                if (CanGothroughWalls == false)
                {
                    SnakeBeatUp();
                }
            }
            for (int i = 0; i < fruitControler.NumberOfFruits; i++)
            {
                Fruit fruit = fruitControler.GetFruitReadonly(i);
                if (Colizions.IsSnakeEatFruit(snake, fruit))
                {
                    fruitControler.FruitEatenBySnake(i);
                    if (fruit.FruitFunctions == FruitFunctions.Points)
                    {
                        Points(fruit.Value);
                    }
                    else if (fruit.FruitFunctions == FruitFunctions.SnakeLenght)
                    {
                        snake.IncreaseLenght(fruit.Value);
                    }
                    else if (fruit.FruitFunctions == FruitFunctions.Live)
                    {
                        Live(fruit.Value);
                    }
                    else if (fruit.FruitFunctions == FruitFunctions.CanEatItself)
                    {
                        CanEatItself = true;
                        this.snake.SetCanEatItself(CanEatItself);
                        time = 0;
                    }
                    else if (fruit.FruitFunctions == FruitFunctions.CanGoThroughWals)
                    {
                        CanGothroughWalls = true;
                        this.mainView.SetCanGothroughWalls(CanGothroughWalls);
                        this.snake.SetCanGothroughWalls(CanGothroughWalls);
                        time = 0;
                    }
                    else {}
                    break;
                }
            }    
        }

        private void Live(int value)
        {
            amountOfLife += value;
            mainView.SetLives(amountOfLife);
        }

        private void Points(int value)
        {
            points += value;
            mainView.SetPoints(points);
        }

        private void Time(int value)
        {
            mainView.SetTime(value);
        }

        private void SnakeBeatUp()
        {
            SetPause(true);
            amountOfLife--;
            if (amountOfLife <= 0)
            {
                isGameOver = true;
                MessageBox.Show("GAME OVER");
            }
            else
            {
                isGameOver = false;
            }
            NewGame();
            mainView.RefreshPanel();
        }

        internal Point GetRandomPoint()
        {//owoce nie losują sie za każdym razem po restarcie
            Size panelSize = mainView.GetPanelSize();
            bool repeat = false;
            int x, y;
            do
            {
                repeat = false;
                x = (random.Next(0, panelSize.Width - 1) / size) * size;
                y = (random.Next(0, panelSize.Height - 1) / size) * size; 

                List<Point> snakeElements = snake.GetBodyLocation();              
                foreach (Point item in snakeElements)
                {
                    if (item.X == x && item.Y == y)
                    {
                        repeat = true;
                        break;
                    }
                }
                if (!repeat)
                {
                    List<Fruit> fruitElements = fruitControler.GetFruitsReadonly();
                    foreach (Fruit item in fruitElements)
                    {
                        if (item.Location.X == x && item.Location.Y == y)
                        {
                            repeat = true;
                            break;
                        }
                    }
                }
            } while (repeat);
            return new Point(x, y);
        }

        internal void Run()
        {
            Application.Run(this.mainView);
        }

        internal void KeyDown(Keys keyCode)
        {
            if (keyCode == Keys.Left || keyCode == Keys.Right || keyCode == Keys.Down || keyCode == Keys.Up)
            {
                this.snake.SetDirection(keyCode);
            }
            else if (keyCode == Keys.P)
            {
                SetPause(!this.pause);
            }
            else if (keyCode == Keys.N)
            {
                isGameOver = true;
                MessageBox.Show("Restart gry. ARE YOU REDY?");
                mainView.SetPause(false);
                NewGame();
            }      
        }

        private void SetPause(bool pause)
        {
            this.pause = pause;
            mainTimer.Enabled = !pause;
            mainView.SetPause(pause);
        }

        internal void DrawSnake(Graphics graphics)
        {            
            this.snake.Draw(graphics);
            this.fruitControler.DrawFruits(graphics);
        }
    }
}
