using System;
using System.Collections.Generic;
using System.IO;
using XmlIO;

namespace ReactiveApp1
{
    /// <summary>
    /// Manages the display
    /// </summary>
    class DisplayProcessor
    {
        /// <summary>
        /// UI template
        /// </summary>
        private string UI = "####################################################################################################\n" + 
            "                                                                                                    \n" +
            "                                                                                                    \n" +
            "                                                                                                    \n" +
            "                                                                                                    \n" +
            "                                                                                                    \n" +
            "                                                                                                    \n" +
            "                                                                                                    \n" +
            "####################################################################################################\n" +
            "   score: ";

        /// <summary>
        /// Game over screen template
        /// </summary>
        private string GMScreen = "####################################################################################################\n" +
            "   GAME OVER                                        Best scores:                                    \n" +
            "                                                                                                    \n" +
            "    score:                                           1-                                             \n" +
            "                                                     2-                                             \n" +
            "                                                     3-                                             \n" +
            "                                                     4-                                             \n" +
            "                                                     5-                                             \n" +
            " Press enter to exit                                                                                \n" +
            "####################################################################################################";

        /// <summary>
        /// Manages the in-game display
        /// </summary>
        public void Draw()
        {
            Console.SetWindowSize(100, 10);
            Console.SetCursorPosition(0, 0);
            DrawUI();
            DrawPlayer();
            DrawObstacles();
        }

        /// <summary>
        /// Draws the UI
        /// </summary>
        private void DrawUI()
        {
            Console.Write(this.UI + Data.Score.ToString());
        }

        /// <summary>
        /// Draws the player
        /// </summary>
        private void DrawPlayer()
        {
            Console.SetCursorPosition(5, Data.YPos);
            Console.Write('@');
        }

        /// <summary>
        /// Draws the visible obstacles
        /// </summary>
        private void DrawObstacles()
        {
            foreach (Obstacle Obs in Data.Obstacles)
            {
                if (Obs.RelativeXPos < 100) //don't display outside the window
                {
                    Console.SetCursorPosition(Obs.RelativeXPos, 7);
                    Console.Write(Obs.Sprite);
                }
            }
        }

        /// <summary>
        /// Draws the Game Over screen
        /// </summary>
        public void GameOver()
        {
            Console.SetWindowSize(100, 10);
            Console.SetCursorPosition(0, 0);
            Console.Write(GMScreen);
            Console.SetCursorPosition(11, 3);
            Console.Write(Data.Score.ToString());
            WriteScore();
        }

        /// <summary>
        /// Displays the 5 best scores and saves the scores to a file
        /// </summary>
        public void WriteScore()
        {
            List<UInt64> ScoreList;
            if (File.Exists("score.xml")) //check if file exists before trying to read
            {
                ScoreList = ListIO.ReadList<UInt64>("score.xml");
                ScoreList.Add(Data.Score);
                ScoreList.Sort();
                ScoreList.Reverse();
                if (ScoreList.Count > 5) //remove the excess scores
                {
                    ScoreList.RemoveAt(5);
                }
            }
            else //generate new file if it deos not exists
            {
                ScoreList = new List<UInt64>
                {
                    Data.Score
                };
            }
            byte Line = 3;
            foreach (UInt64 CurrScore in ScoreList) //write scores
            {
                Console.SetCursorPosition(56, Line);
                Console.Write(CurrScore.ToString());
                Line += 1;
            }
            ListIO.WriteList<UInt64>("score.xml", ScoreList);
            ScoreList = null;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public DisplayProcessor()
        {

        }
    }
}
