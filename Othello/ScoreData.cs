using System;

namespace Othello
{
    /** <summary>
     * Custom class for storing high scores.
     * </summary>
     */
    public class ScoreData
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public string Winner { get; set; }
        public string Time { get; set; }
    }
}