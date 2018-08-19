using System;
using System.Collections.Generic;

namespace ReactiveApp1
{
    /// <summary>
    /// Stores global program data
    /// </summary>
    static class Data
    {
        /// <summary>
        /// Stores the last registered input as an understandable string
        /// </summary>
        public static string CurrentInput;
        /// <summary>
        /// Stores the game score
        /// </summary>
        public static UInt64 Score;
        /// <summary>
        /// Stores the altitude of the player
        /// </summary>
        public static byte YPos;
        /// <summary>
        /// Stores the current moment in the jump, 0 when not jumping
        /// </summary>
        public static byte JumpState;
        /// <summary>
        /// Stores all the currently existing obstacles
        /// </summary>
        public static List<Obstacle> Obstacles;
        /// <summary>
        /// Stores the minimum distance between two obstacles
        /// </summary>
        public static byte SafeDistance;

        /// <summary>
        /// Sets Data's values to their defaults
        /// </summary>
        public static void InitData()
        {
            CurrentInput = "none";
            Score = 0;
            YPos = 7;
            JumpState = 0;
            Obstacles = new List<Obstacle>();
            SafeDistance = 5;
        }
    }
}
