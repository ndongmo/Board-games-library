using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGL.Client.Model
{
    /// <summary>
    /// Game state enumeration
    /// </summary>
    public enum GameState { WON, LOST, DRAW, CONTINUE}
    /// <summary>
    /// Game main class.
    /// </summary>
    public abstract class Game
    {
        public abstract void reset();
        public abstract void setLastMove(Object move, int player);
        public abstract GameState updateMove();
    }
}
