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

        public int X { get; set; }
        public int Y { get; set;  }
    }
    class Program
    {        
        static void Main(string[] args)
        {
            #region Map Data
            string[,] map =
            {
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " },
                {". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". " }
            };
            #endregion

            int highScore = 0;
            ConsoleKeyInfo keyPress;

            do
            {
                #region Game init
                Point direction = new Point(1, 0);
                List<Point> snake = new List<Point>();
                snake.Add(new Point(4, 4));
                snake.Add(new Point(4, 4));
                snake.Add(new Point(4, 4));
                Point candy = new Point(8, 8);

                int gameSpeed = 500;
                int score = 0;
                CursorVisible = false;
                var random = new Random();

                //Only supported on windows machines
                SetWindowSize(40, 21);
                SetBufferSize(40, 21);

                IntroText();
                ConsoleKeyInfo keyPressed = ReadKey(true);
                #endregion

                #region Game loop
                while (true)
                {
                    #region Draw map
                    Clear();
                    ForegroundColor = ConsoleColor.Gray;
                    WriteLine($"Score: { score }             HI: { highScore }");
                    ForegroundColor = ConsoleColor.Green;
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
                            else if (candy.X == currentPosition.X && candy.Y == currentPosition.Y)
                            {
                                BackgroundColor = ConsoleColor.Red;
                                Write("  ");
                                BackgroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Write(map[y, x]);
                            }
                        }
                    }
                    #endregion

                    #region Input
                    Stopwatch sw = Stopwatch.StartNew();
                    while (sw.ElapsedMilliseconds < gameSpeed)
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

                    if (newPosition.X < 0 || newPosition.X >= 20 || newPosition.Y < 0 || newPosition.Y >= 20)
                    {
                        break;
                    }

                    //TODO: Fix snake collision
                    //for (int i = 1; i < snake.Count; i++)
                    //{
                    //    if (head.X == snake[i].X && head.Y == snake[i].Y)
                    //    {
                    //        break;
                    //    }
                    //}

                    snake.Insert(0, newPosition);

                    if (head.X == candy.X && head.Y == candy.Y)
                    {
                        candy.X = random.Next(1, 19);
                        candy.Y = random.Next(1, 19);
                        score += 10;
                        gameSpeed -= 25;
                    }
                    else
                    {
                        snake.RemoveAt(snake.Count - 1);
                    }
                    #endregion
                }
                #endregion

                #region Game Over
                GameOver();

                if (score > highScore)
                {
                    highScore = score;
                }

                keyPress = ReadKey(true);
                #endregion

            } while (keyPress.Key == ConsoleKey.Y);

            Clear();            
        }          

        private static void IntroText()
        {
            Clear();
            ForegroundColor = ConsoleColor.Green;
            SetCursorPosition(0, 4);
            WriteLine(@"       _____ _   _____    __ __ ______");
            WriteLine(@"      / ___// | / /   |  / //_// ____/");
            WriteLine(@"      \__ \/  |/ / /| | / ,<  / __/   ");
            WriteLine(@"     ___/ / /|  / ___ |/ /| |/ /___   ");
            WriteLine(@"    /____/_/ |_/_/  |_/_/ |_/_____/   ");
            SetCursorPosition(0, 10);
            WriteLine("           The retro way 2021");
            WriteLine("      Use Left and Right arrow key");
            WriteLine();
        }

        private static void GameOver()
        {
            Clear();
            SetCursorPosition(0, 4);
            WriteLine(@"     _________    __  _________");
            WriteLine(@"    / ____/   |  /  |/  / ____/");
            WriteLine(@"   / / __/ /| | / /|_/ / __/   ");
            WriteLine(@"  / /_/ / ___ |/ /  / / /___   ");
            WriteLine(@"  \____/_/  |_/_/  /_/_____/   ");
            SetCursorPosition(0, 9);
            WriteLine(@"                ____ _    ____________ ");            
            WriteLine(@"               / __ \ |  / / ____/ __ \");            
            WriteLine(@"              / / / / | / / __/ / /_/ /");            
            WriteLine(@"             / /_/ /| |/ / /___/ _, _/ ");            
            WriteLine(@"             \____/ |___/_____/_/ |_|  ");
            SetCursorPosition(0, 16);
            WriteLine("            Play again Y/N?");
        }
    }
}
