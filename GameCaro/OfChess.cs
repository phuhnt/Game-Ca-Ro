using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    /// <summary>
    /// Lớp: Ô cờ
    /// </summary>
    public class OfChess
    {
        public const int _width = 25; //Chiều rộng ô cờ
        public const int _height = 25; // Chiều cao ô cờ

        private int _row;
        private int _column;
        private Point _position;
        private int _selected;

        public OfChess()
        {

        }

        public OfChess(int row, int col, Point pos, int sel)
        {
            _row = row;
            _column = col;
            _position = pos;
            _selected = sel;
        }

        public int Row { get => _row; set => _row = value; }
        public int Column { get => _column; set => _column = value; }
        public Point Position { get => _position; set => _position = value; }
        public int Selected { get => _selected; set => _selected = value; }

    }
}
