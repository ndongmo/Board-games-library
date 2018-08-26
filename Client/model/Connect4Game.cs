using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TGL.Client.Model;

namespace GCM.model
{
    /// <summary>
    /// Connect4 game class.
    /// This algorithm is inspired from Dominikus Herzberg connect4 algorithm
    /// https://github.com/ndongmo/BitboardC4/blob/master/BitboardDesign.md
    /// and also https://github.com/qu1j0t3/fhourstones
    /// </summary>
    public class Connect4Game : Game
    {
        public const int WIDTH = 7;
        public const int HEIGHT = 6;
        public const int H1 = HEIGHT + 1;
        public const int H2 = HEIGHT + 2;
        public const int SIZE = HEIGHT * WIDTH;
        public const int SIZE1 = H1 * WIDTH;
        public const long ALL1 = (1L << SIZE1) - 1L; // assumes SIZE1 < 63
        public const int COL1 = (1 << H1) - 1;
        public const long BOTTOM = ALL1 / COL1; // has bits i*H1 set
        public const long TOP = BOTTOM << HEIGHT;

        private long[] board;  // black and white bitboard
        private int nplies;
        private int[] moves;
        private byte[] height; // holds bit index of lowest free square

        public Connect4Game()
        {
            board = new long[2];
            height = new byte[WIDTH];
            moves = new int[SIZE];
            reset();
        }

        public override void reset()
        {
            nplies = 0;
            board[0] = board[1] = 0L;
            for (int i = 0; i < WIDTH; i++)
                height[i] = (byte)(H1 * i);
        }

        public long positioncode()
        {
            return 2 * board[0] + board[1] + BOTTOM;
            // board[0] + board[1] + BOTTOM forms bitmap of heights
            // so that positioncode() is a complete board encoding
        }

        // return whether columns col has room
        public bool isplayable(int col)
        {
            return islegal(board[nplies & 1] | (1L << height[col]));
        }

        public bool isplayable()
        {
            for (int j = 0; j < WIDTH; j++)
                if (isplayable(j))
                    return true;
            return false;
        }

        // return whether newboard lacks overflowing column
        private bool islegal(long newboard)
        {
            return (newboard & TOP) == 0;
        }

        // return whether newboard is legal and includes a win
        private bool islegalhaswon(long newboard)
        {
            return islegal(newboard) && haswon(newboard);
        }

        // return whether newboard includes a win
        private bool haswon(long newboard)
        {
            long y = newboard & (newboard >> HEIGHT);
            if ((y & (y >> 2 * HEIGHT)) != 0) // check diagonal \
                return true;
            y = newboard & (newboard >> H1);
            if ((y & (y >> 2 * H1)) != 0) // check horizontal -
                return true;
            y = newboard & (newboard >> H2); // check diagonal /
            if ((y & (y >> 2 * H2)) != 0)
                return true;
            y = newboard & (newboard >> 1); // check vertical |
            return (y & (y >> 2)) != 0;
        }

        void backmove()
        {
            int n;

            n = moves[--nplies];
            board[nplies & 1] ^= 1L << --height[n];
        }

        void makemove(int n, int player)
        {
            board[nplies & 1] ^= 1L << height[n]++;
            moves[nplies++] = n;
        }

        public long[] Board { get { return board; } }

        public override GameState updateMove()
        {
            if (haswon(board[0]) || haswon(board[1]))
                return GameState.WON;
            if (isplayable())
                return GameState.CONTINUE;
            return GameState.DRAW;
        }

        public override void setLastMove(object move, int player)
        {
            makemove((int)move, player);
        }
    }
}
