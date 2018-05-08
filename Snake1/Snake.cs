using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake1
{
    public class Snake
    {
        Size boardSize;
        List<Point> elements;
        Keys direction;
        private int size;
        private int length;
        bool canEatItself;
        bool canGothroughWalls;

        Image imageSnake = Image.FromFile("Images\\BigEyesSnake.jpg");

        public Snake(Point location, int size, Size boardSize)
        {
            this.size = size;
            this.boardSize = boardSize;
            this.length = 3;

            elements = new List<Point>();
            for (int i = 0; i < length; i++)
            {
                elements.Add(new Point(location.X, location.Y + i * size));
            }
            this.direction = Keys.Up;

            canEatItself = false;
            canGothroughWalls = false;
        }

        internal Point GetHeadLocation()
        {
            return new Point(elements[0].X, elements[0].Y);
        }

        internal List<Point> GetBodyLocation()
        {
            List<Point> points = new List<Point>();

            foreach (Point item in elements)
            {
                points.Add(new Point(item.X, item.Y));
            }
            return points;
        }

        public void IncreaseLenght(int length)
        {
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    elements.Add(new Point(elements[elements.Count - 1].X, elements[elements.Count - 1].Y));
                }
            }
            else
            {
                try
                {
                    elements.RemoveRange(elements.Count - 1 - Math.Abs(length), Math.Abs(length));
                }
                catch//jesli wąż ma 0 powien stracić życie, w tym przypadku nic sie nie stanie
                {
                    elements.RemoveRange(elements.Count - 1, Math.Abs(0));    
                }
            }
        }

        public void Move()
        {
            Point step = Point.Empty;
            if (direction == Keys.Left)
            {
                step = new Point(-size, 0);
            }
            else if (direction == Keys.Right)
            {
                step = new Point(size, 0);
            }
            else if (direction == Keys.Up)
            {
                step = new Point(0, -size);
            }
            else if (direction == Keys.Down)
            {
                step = new Point(0, size);
            }

            //if (elements[0].X + step.X >= 0 && elements[0].X + step.X < boardSize.Width
            //   && elements[0].Y + step.Y >= 0 && elements[0].Y + step.Y < boardSize.Height)//Wąż nie może włazić na ścianę
            if (step != Point.Empty)
            {
                for (int i = elements.Count - 1; i > 0; i--)
                {
                    Point tempPoint = this.elements[i];
                    tempPoint.X = this.elements[i - 1].X;
                    tempPoint.Y = this.elements[i - 1].Y;
                    this.elements[i] = tempPoint;
                }
                // class
                //elements[0].X += step.X;
                //elements[0].Y += step.Y;

                // struct
                Point temp = elements[0];
                temp.X += step.X;
                temp.Y += step.Y;
                elements[0] = temp;

                if (canGothroughWalls)//Wąż może przychodzić przez ściany 
                {
                    if (elements[0].X < 0)
                    {
                        temp = elements[0];
                        temp.X = boardSize.Width-size;
                        elements[0] = temp;
                    }
                    else if (elements[0].X >= boardSize.Width)
                    {
                        temp = elements[0];
                        temp.X = 0;
                        elements[0] = temp;
                    }
                    else if (elements[0].Y < 0)
                    {
                        temp = elements[0];
                        temp.Y = boardSize.Height - size;
                        elements[0] = temp;
                    }
                    else if (elements[0].Y >= boardSize.Height)
                    {
                        temp = elements[0];
                        temp.Y = 0;
                        elements[0] = temp;
                    }
                }
            }
        }

        internal void SetDirection(Keys keyCode)
        {
            this.direction = keyCode;
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (i==0)
                {
                    g.DrawImage(imageSnake, this.elements[i].X, this.elements[i].Y, this.size, this.size);
                }
                else
                {
                    if (!canEatItself)
                    {
                        g.FillRectangle(Brushes.Chocolate, this.elements[i].X, this.elements[i].Y, this.size, this.size);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Green, this.elements[i].X, this.elements[i].Y, this.size, this.size);
                    }
                }
            }
        }

        internal void SetCanEatItself(bool canEatItself)
        {
            this.canEatItself = canEatItself;
        }

        internal void SetCanGothroughWalls(bool canGothroughWalls)
        {
            this.canGothroughWalls = canGothroughWalls;
        }
    }
}
