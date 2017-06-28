using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class fmMain : Form
    {
        private ChessCaro _chessCaro;
        private Graphics gra;

        public fmMain()
        {
            InitializeComponent();
            _chessCaro = new ChessCaro();
            _chessCaro.ofChessArrInit();
            gra = pnChessBoard.CreateGraphics();
        }

        public fmMain(int playerMode)
        {
            InitializeComponent();
            _chessCaro = new ChessCaro();
            _chessCaro.ofChessArrInit();
            gra = pnChessBoard.CreateGraphics();

            if (playerMode == 1) // Chế độ chơi với máy PvC
            {
                gra.Clear(pnChessBoard.BackColor);
                _chessCaro.StartPvsC(gra);
            }
            else if (playerMode == 2) // Chới độ chơi với người PvP
            {
                gra.Clear(pnChessBoard.BackColor);
                _chessCaro.StartPvsP(gra);
            }
        }
        private void fmMain_Load(object sender, EventArgs e)
        {

        }

        private void pnChessBoard_Paint(object sender, PaintEventArgs e)
        {
            _chessCaro.DrawChessBoard(gra);
            _chessCaro.drawChessmanAgain(gra);
        }

        private void pnChessBoard_MouseClick(object sender, MouseEventArgs e)
        {

            if (!_chessCaro.Ready)
                return;

            // Người đánh
            _chessCaro.check(e.X, e.Y, gra);

            if (_chessCaro.CheckForTheWin())
            {
                _chessCaro.EndGame();
                return;
            }

            // Chơi với máy
            if (_chessCaro.Mode == 1)
            {
                _chessCaro.ComputerInit(gra);

                if (_chessCaro.CheckForTheWin())
                {
                    _chessCaro.EndGame();
                    return;
                }
            }
        }

        /// <summary>
        /// Chế độ: Player vs Computer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerVsComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gra.Clear(pnChessBoard.BackColor);
            _chessCaro.StartPvsC(gra);
        }

        /// <summary>
        /// Chế độ: Player vs Player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gra.Clear(pnChessBoard.BackColor);
            _chessCaro.StartPvsP(gra);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _chessCaro.Undo(gra);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _chessCaro.Redo(gra);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Show();
        }
    }
}
