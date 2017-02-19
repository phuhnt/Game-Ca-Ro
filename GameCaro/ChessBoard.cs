using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    /// <summary>
    /// Lớp: bàn cờ
    /// </summary>
    public class ChessBoard
    {
        private int _numOfRow; // Số dòng 
        private int _numOfColumn; // Số cột

        public int NumOfRow { get => _numOfRow; set => _numOfRow = value; }
        public int NumOfColumn { get => _numOfColumn; set => _numOfColumn = value; }

        public ChessBoard()
        {
            NumOfRow = 0;
            NumOfColumn = 0;
        }

        public ChessBoard(int numOfRow, int numOfColumn)
        {
            NumOfRow = numOfRow;
            NumOfColumn = numOfColumn;
        }

        /// <summary>
        /// Phương thức: vẽ bàn cờ
        /// </summary>
        /// <param name="gra"></param>
        public void drawChessBoard(Graphics gra)
        {
            // Vẽ chiều dọc
            for (int i=0; i <= NumOfColumn; i++)
            {
                gra.DrawLine(ChessCaro._pen, i * OfChess._width, 0, i * OfChess._width, NumOfRow * OfChess._height);
            }

            // Vẽ chiều ngang
            for (int j=0; j <= NumOfRow; j++)
            {
                gra.DrawLine(ChessCaro._pen, 0, j * OfChess._height, NumOfColumn * OfChess._width, j * OfChess._height);
            }
        }

        /// <summary>
        /// Phương thức: Vẽ quân cờ
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="point"></param>
        /// <param name="solid"></param>
        public void drawChessman(Graphics gra, Point point, SolidBrush solid)
        {
            gra.FillEllipse(solid, point.X + 1, point.Y + 1, OfChess._width - 2, OfChess._height - 2);
        }

        public void deleteChessman(Graphics gra, Point point, SolidBrush solid)
        {
            gra.FillRectangle(solid, point.X + 1, point.Y + 1, OfChess._width - 2, OfChess._height - 2);
        }
    }
}
