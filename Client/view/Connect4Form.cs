using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TGL.Client.Model;
using GCM.model;

namespace GCM.view
{
    public partial class Connect4Form : ClientForm
    {
        private Connect4Game game;

        public Connect4Form():base()
        {
            InitializeComponent();
            game = new Connect4Game();
        }

        protected override void reset() { if(game != null) game.reset(); }
        protected override void setLastMove(Object move, int player) { game.setLastMove(move, player); }
        protected override GameState updateMove() { return game.updateMove(); }

        protected override void draw()
        {
            base.draw();
            panelGame.Refresh();
        }

        /*
         * variables signification :
         * mw : margin width; mh : margin height; 
         * w : panel width; h : panel height; cw : column total width; ch : column total height;
         * pw : width position to draw; ph : height position to draw;
         */
        private void panelGame_Paint(object sender, PaintEventArgs e)
        {
            int mw = 10, mh = 10, w = panelGame.Width, h = panelGame.Height;
            int pw = (w / Connect4Game.WIDTH), 
                ph = (h / Connect4Game.HEIGHT),
                cw = (w / Connect4Game.WIDTH) - mw, 
                ch = (h / Connect4Game.HEIGHT) - mh;

            SolidBrush brush = new SolidBrush(Color.Blue);
            Rectangle rect = new Rectangle(0, 0, panelGame.Width, panelGame.Height);
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(e.Graphics, rect);

            buffer.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            buffer.Graphics.FillRectangle(brush, rect);

            for (int i = Connect4Game.HEIGHT-1, ii=0; i >=0; i--, ii++)
            {
                for (int j = i, jj = 0; j < Connect4Game.SIZE1; j += Connect4Game.H1, jj++)
                {
                    long mask = 1L << j;

                    if ((game.Board[0] & mask) != 0)
                        brush.Color = Color.Red;
                    else if ((game.Board[1] & mask) != 0)
                        brush.Color = Color.Yellow;
                    else
                        brush.Color = Color.White;

                    buffer.Graphics.FillEllipse(brush, jj * pw + (mw / 2), ii * ph + (mh / 2), cw, ch);
                }
            }

            buffer.Render();
            buffer.Dispose();
        }

        private void panelGame_MouseClick(object sender, MouseEventArgs e)
        {
            int col = (int)Math.Floor(e.X / (panelGame.Width / (double)Connect4Game.WIDTH));
            if(game.isplayable())
                moveGame(col, 0);
        }
    }
}
