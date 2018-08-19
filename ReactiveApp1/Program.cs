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
            Data.InitData(); //sets data to default values
            Random Rand = new Random();
            DisplayProcessor Display = new DisplayProcessor();
            Thread InputProcessor = new Thread(InputProcessing.ProcessInput) //input is processed in a background thread to avoid blocking main execution
            {
                IsBackground = true
            };
            InputProcessor.Start();
            while (Data.CurrentInput != "esc") //main game loop
            {
                if (ProcessObstacles(Rand)) //detect death
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
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { } //avoid closing the window immediatly
        }

        /// <summary>
        /// Processes everything related to the obstacles, their creation, disparition and their collision
        /// </summary>
        /// <param name="Rand"></param>
        /// <returns>Returns true if the player dies on an obstacle, false otherwise</returns>
        public static bool ProcessObstacles(Random Rand)
        {
            foreach (Obstacle Obs in Data.Obstacles) //process obstacle movement
            {
                Obs.ProcessObstacle();
                if (Obs.RelativeXPos == 5 && Data.YPos == 7)
                {
                    return true;
                }
            }
            if (Data.SafeDistance > 0) //avoid generating obsatcles too close to each other
            {
                Data.SafeDistance -= 1;
            }
            if (Data.SafeDistance == 0 && Data.Obstacles.Count < 5 && Rand.Next(20) == 0) //generate new obstacles
            {
                Data.Obstacles.Add(new Obstacle());
                Data.SafeDistance = 5;
            }
            if (Data.Obstacles.Count > 0) //avoid generating to many obstacles
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
            if ((Data.JumpState == 0 && Data.CurrentInput == "up") || Data.JumpState == 1) //start of the jump
            {
                Data.JumpState += 1;
                Data.YPos -= 1;
            }
            else if (Data.JumpState == 2 || Data.JumpState == 3 || Data.JumpState == 4) //middle of the jump
            {
                Data.JumpState += 1;
            }
            else if (Data.JumpState == 5 || Data.JumpState == 6) //end of the jump
            {
                Data.JumpState += 1;
                Data.YPos += 1;
            }
            else if (Data.JumpState >= 7) //reset jumpstate
            {
                Data.JumpState = 0;
            }
            Data.CurrentInput = "none";
        }
    }
}
