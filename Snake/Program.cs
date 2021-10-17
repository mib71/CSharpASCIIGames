using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;

namespace Snake
{
    struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
    class Program
    {        
        static void Main(string[] args)
        {
            #region Map Data
            string[,] map =
            {
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " }
            };
            #endregion

            #region Game init
            Point direction = new Point(1, 0);
            List<Point> snake = new List<Point>();
            snake.Add(new Point(4, 4));
            snake.Add(new Point(4, 4));
            snake.Add(new Point(4, 4));

            //Only supported on windows machines
            SetWindowSize(20, 11);
            SetBufferSize(20, 11);
            #endregion

            #region Game loop
            while (true)
            {
                #region Draw map
                Clear();                
                for (int y = 0; y < map.GetLength(0); y++)
                {                    
                    for (int x = 0; x < map.GetLength(1); x++)
                    {
                        Point currentPosition = new Point(x, y);
                        if (snake.Contains(currentPosition))
                        {
                            BackgroundColor = ConsoleColor.Green;
                            Write("  ");
                            BackgroundColor = ConsoleColor.Black;
                        }
                        else
                            Write(map[y, x]);
                    }
                    WriteLine();
                }
                #endregion

                #region Input
                Stopwatch sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < 500)
                {
                    if (KeyAvailable)
                    {
                        var key = ReadKey(true);
                        if (key.Key == ConsoleKey.LeftArrow)
                        {
                            direction = new Point(direction.Y, -direction.X);                            
                        }

                        if (key.Key == ConsoleKey.RightArrow)
                        {
                            direction = new Point(-direction.Y, direction.X);
                        }
                    }
                }
                #endregion

                #region  Movement ang logic
                Point head = snake[0];
                Point newPosition = new Point(head.X + direction.X, head.Y + direction.Y);
                snake.Insert(0, newPosition);
                snake.RemoveAt(snake.Count - 1);
                #endregion
            }
            #endregion
        }
    }
}
