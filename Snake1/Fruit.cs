using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake1
{
    public enum FruitFunctions
    {
        Points,
        SnakeLenght,
        Live,
        CanEatItself,
        CanGoThroughWals
    }
    public class Fruit
    {
        public Point Location { get; private set; }
        public Brush Brush { get; private set; }
        public int Size { get; private set; }
        public FruitFunctions FruitFunctions { get; private set; }
        public int Value { get; private set; }

        public Fruit(Point location, int size, Brush brush, FruitFunctions fruitFunctions, int value)
        {
            Location = location;
            Size = size;
            Brush = brush;
            this.FruitFunctions = fruitFunctions;
            this.Value = value;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(this.Brush, this.Location.X, this.Location.Y, this.Size, this.Size);
        }
    }
}
