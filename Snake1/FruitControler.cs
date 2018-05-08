using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake1
{   
    public class FruitControler
    {
        MainControler mainControler;
        List<Fruit> fruits;
        private Size boardSize;
        private Random random;
        public int NumberOfFruits { get { return this.fruits.Count; } }
        private int elementSize;

        public FruitControler(int elementSize, Size boardSize, MainControler mainControler)
        {
            this.elementSize = elementSize;
            this.boardSize = boardSize;
            this.mainControler = mainControler;
            this.fruits = new List<Fruit>();
            this.random = new Random();
        }

        public void InitFruit(int initialNumberOfFruits)
        {
            fruits.Clear();
            for (int i = 0; i < initialNumberOfFruits; i++)
            {
                this.fruits.Add(GetRandomFruit());
            }
        }

        private Fruit GetRandomFruit()
        {
            Point p = mainControler.GetRandomPoint();
            FruitFunctions ff = GetFruitFunction();
            int i = GetValue(ff);
            Brush b = GetBrush((int)ff);
            return new Fruit(p, elementSize, b, ff, i);
        }

        private int GetValue(FruitFunctions ff)
        {
            if (ff == FruitFunctions.Points)
            {
                int p = this.random.Next(0, 10);
                if (p >= 0 && p < 6)
                {
                    return 10;
                }
                else if (p >= 6 && p < 8)
                {
                    return 20;
                }
                else if (p == 8)
                {
                    return 30;
                }
                else
                {
                    return -10;
                }
            }
            else if (ff == FruitFunctions.SnakeLenght)
            {
                int sl = this.random.Next(0, 10);
                if (sl >= 0 && sl < 6)
                {
                    return 1;
                }
                else if (sl >= 6 && sl < 8)
                {
                    return 2;
                }
                else
                {
                    return -1;
                }
            }
            else if (ff == FruitFunctions.Live)
            {
                int l = this.random.Next(0, 10);
                if (l >= 0 && l < 6)
                {
                    return 1;
                }
                else if (l >= 6 && l < 8)
                {
                    return 2;
                }
                else
                {
                    return -1;
                }
            }else
            {
                return 0;
            }
        }

        private FruitFunctions GetFruitFunction()
        {
            int ff = this.random.Next(0, 100);

            if (ff >= 0 && ff < 60 )
            {
                return FruitFunctions.Points;
            }
            else if (ff >= 60 && ff < 80)
            {
                return FruitFunctions.SnakeLenght;
            }
            else if (ff >= 80 && ff < 86)
            {
                return FruitFunctions.Live;
            }
            else if (ff >= 86 && ff < 93)
            {
                return FruitFunctions.CanEatItself;
            }
            else
            { 
                return FruitFunctions.CanGoThroughWals;
            }
        }

        internal Brush GetBrush(int color)
        {
            switch (color)
            {
                case 0:
                    return Brushes.Red;
                case 1:
                    return Brushes.Black;
                case 2:
                    return Brushes.Blue;
                case 3:
                    return Brushes.Green;
                default:
                    return Brushes.Yellow;
            }
        }

        public void DrawFruits(Graphics graphics)
        {
            for (int i = 0; i < NumberOfFruits; i++)
            {
                this.fruits[i].Draw(graphics);
            }       
        }

        public List<Fruit> GetFruitsReadonly()
        {
            List<Fruit> retList = new List<Fruit>();
            foreach (Fruit item in fruits)
            {
                retList.Add(new Fruit(item.Location, item.Size, item.Brush, item.FruitFunctions, item.Value));
            }
            return retList; 
        }

        public Fruit GetFruitReadonly(int index)
        {
            Fruit fruit = new Fruit(fruits[index].Location, fruits[index].Size, fruits[index].Brush, fruits[index].FruitFunctions, fruits[index].Value);
            return fruit;
        }

        public void FruitEatenBySnake(int index)
        {
            fruits[index] = GetRandomFruit();
        }
    }
}
