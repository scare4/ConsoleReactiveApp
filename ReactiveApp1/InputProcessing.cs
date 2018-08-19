using System;
using System.Threading;

namespace ReactiveApp1
{
    /// <summary>
    /// Manages the inputs as a background thread
    /// </summary>
    class InputProcessing
    {
        /// <summary>
        /// Thread entry point, holds the thread's main loop
        /// </summary>
        public static void ProcessInput()
        {
            while (Data.CurrentInput != "esc")
            {
                if (Console.KeyAvailable) //only check input if there is input to be read
                {
                    Data.CurrentInput = GetInputKey();
                }
                Thread.Sleep(25);
            }
        }

        /// <summary>
        /// Checks if the detected input is registered as an input
        /// </summary>
        /// <returns>Returns a string that will be stored in Data and be directly usable by other classes</returns>
        static string GetInputKey()
        {
            if (Data.CurrentInput == "none") //avoid changing current input before the previous one has been read
            {
                if (Console.ReadKey(true).Key == ConsoleKey.UpArrow)
                {
                    return "up";
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    return "esc";
                }
                else
                {
                    return Data.CurrentInput;
                }
            }
            else
            {
                return Data.CurrentInput;
            }
        }
    }
}
