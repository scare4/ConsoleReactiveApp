using System;
using System.Threading;

namespace ReactiveApp1
{
    /// <summary>
    /// Main program class, holds all core process methods
    /// </summary>
    class Program
    {
        /// <summary>
        /// Program entry point, also contains the main program loop
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Data.InitData();
            Random Rand = new Random();
            DisplayProcessor Display = new DisplayProcessor();
            Thread InputProcessor = new Thread(InputProcessing.ProcessInput)
            {
                IsBackground = true
            };
            InputProcessor.Start();
            while (Data.CurrentInput != "esc")
            {
                if (ProcessObstacles(Rand))
                {
                    break;
                }
                Display.Draw();
                Data.Score += 1;
                ProcessPlayer();
                Thread.Sleep(50);
            }
            Display.GameOver();
            InputProcessor.Abort();
            Rand = null;
            Display = null;
            Data.Obstacles = null;
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }

        /// <summary>
        /// Processes everything related to the obstacles, their creation, disparition and their collision
        /// </summary>
        /// <param name="Rand"></param>
        /// <returns>Returns true if the player dies on an obstacle, false otherwise</returns>
        public static bool ProcessObstacles(Random Rand)
        {
            foreach (Obstacle Obs in Data.Obstacles)
            {
                Obs.ProcessObstacle();
                if (Obs.RelativeXPos == 5 && Data.YPos == 7)
                {
                    return true;
                }
            }
            if (Data.SafeDistance > 0)
            {
                Data.SafeDistance -= 1;
            }
            if (Data.SafeDistance == 0 && Data.Obstacles.Count < 5 && Rand.Next(20) == 0)
            {
                Data.Obstacles.Add(new Obstacle());
                Data.SafeDistance = 5;
            }
            if (Data.Obstacles.Count > 0)
            {
                if (Data.Obstacles[0].RelativeXPos == 0)
                {
                    Data.Obstacles.RemoveAt(0);
                }
            }
            return false;
        }

        /// <summary>
        /// Processes the player's movement
        /// </summary>
        public static void ProcessPlayer()
        {
            if ((Data.JumpState == 0 && Data.CurrentInput == "up") || Data.JumpState == 1)
            {
                Data.JumpState += 1;
                Data.YPos -= 1;
            }
            else if (Data.JumpState == 2 || Data.JumpState == 3 || Data.JumpState == 4)
            {
                Data.JumpState += 1;
            }
            else if (Data.JumpState == 5 || Data.JumpState == 6)
            {
                Data.JumpState += 1;
                Data.YPos += 1;
            }
            else if (Data.JumpState >= 7)
            {
                Data.JumpState = 0;
            }
            Data.CurrentInput = "none";
        }
    }
}
