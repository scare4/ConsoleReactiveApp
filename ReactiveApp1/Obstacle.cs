namespace ReactiveApp1
{
    /// <summary>
    /// Obstacle class
    /// </summary>
    public class Obstacle
    {
        /// <summary>
        /// Stores the altitude of the obstacle
        /// </summary>
        public byte YPos;
        /// <summary>
        /// Stores the horisontal offset from the left side of the screen of the obstacle
        /// </summary>
        public byte RelativeXPos;
        /// <summary>
        /// Stores the obstacle's sprite
        /// </summary>
        public char Sprite;

        /// <summary>
        /// Class constructor, initiates the obstacle's data
        /// </summary>
        public Obstacle()
        {
            RelativeXPos = 101;
            YPos = 0;
            Sprite = '&';
        }

        /// <summary>
        /// Processes the obstacle's movement
        /// </summary>
        public void ProcessObstacle()
        {
            if (RelativeXPos > 0) //delete the obstacle when it reached the very left of the screen
            {
                RelativeXPos -= 1;
            }
        }
    }
}
