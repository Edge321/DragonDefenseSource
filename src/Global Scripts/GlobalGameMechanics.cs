using System.Collections;
using System.Collections.Generic;

namespace GlobalGameMechanics
{
    /// <summary>
    /// Class that contains <c>static</c> variables used during gameplay
    /// </summary>
    class GameBehaviors
    {
        /// <summary>
        /// Bool for if player can move in game
        /// </summary>
        public static bool Moveable { get; set; } = false;
        /// <summary>
        /// Bool for if game is over
        /// </summary>
        public static bool GameOver { get; set; } = false;
        /// <summary>
        /// Bool for if the game was resetted
        /// </summary>
        public static bool Reset { get; set; } = false;
        /// <summary>
        /// Limited space the player has on the X-coordinate
        /// </summary>
        public static int RandomX { get; set; }
        /// <summary>
        /// Score for current game session
        /// </summary>
        public static int GameScore { get; set; } = 0;
        /// <summary>
        /// Highscore for the total game session
        /// </summary>
        public static int HighScore { get; set; } = 0;
        /// <summary>
        /// Difficulty for the game session
        /// </summary>
        public static Difficulties Difficulty { get; set; }

        /// <summary>
        /// Array that keeps track of all enemies and projectiles in game.
        /// Used for if the game is over, then destroy all enemies and projectiles in the array
        /// </summary>
        public static ArrayList objectList = new ArrayList();
        
    }
    /// <summary>
    /// Contains the enum difficulties that can be chosen in game
    /// </summary>
    public enum Difficulties
    {
        EASY, NORMAL, HARD
    }
}
