using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake1
{
    public static class Colizions
    {
        public static bool IsSnakeEatFruit(Snake snake, Fruit fruit)
        {
            Point fruitLocation = fruit.Location;
            Point snakeHead = snake.GetHeadLocation(); 

            if (fruitLocation == snakeHead)
            {
                return true;
            }
            return false;
        }

        public static bool IsSnakeColidedWithBoards(Snake snake, Size boardSize)
        {
            Point snakeHead = snake.GetHeadLocation();

            if (snakeHead.X < 0 || snakeHead.Y < 0 || snakeHead.X >= boardSize.Width || snakeHead.Y >= boardSize.Height)
            {
                return true;
            }
            return false;
        }

        public static bool IsSnakeColidetWithItSelf(Snake snake)
        {
            List<Point> snakeBody = snake.GetBodyLocation();

            for (int i = 1; i < snakeBody.Count; i++)
            {
                if (snakeBody[0].X == snakeBody[i].X && snakeBody[0].Y == snakeBody[i].Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
