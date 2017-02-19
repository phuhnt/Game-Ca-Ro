using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public enum Result
    {
        Draw,       // Hòa cờ
        Player1,    // Người chơi 1 chiến thắng
        Player2,    // Người chơi 2 chiến thắng
        Com         // Máy thắng
    }

    public class ChessCaro
    {
        private ChessBoard _chessBoard;
        private OfChess[,] _ofChessArr;
        public static Pen _pen;
        public static SolidBrush _solidRed;
        public static SolidBrush _solidBlue;
        public static SolidBrush _solidChessBoard;
        private Stack<OfChess> _checkedStack;
        private Stack<OfChess> _undoStack;
        private int _turn; // Lượt đi
        private bool _ready;
        private Result _result;
        private int _mode;

        public bool Ready { get => _ready; set => _ready = value; }
        public int Mode { get => _mode; set => _mode = value; }

        public ChessCaro()
        {
            _chessBoard = new ChessBoard(20, 20);
            _ofChessArr = new OfChess[_chessBoard.NumOfRow, _chessBoard.NumOfColumn];
            _pen = new Pen(Color.Black);
            _solidRed = new SolidBrush(Color.Red);
            _solidBlue = new SolidBrush(Color.Blue);
            _solidChessBoard = new SolidBrush(Color.FromArgb(195, 166, 124));
            _checkedStack = new Stack<OfChess>();
            _undoStack = new Stack<OfChess>();
            _turn = 1;
        }

        public void DrawChessBoard(Graphics gra)
        {
            _chessBoard.drawChessBoard(gra);
        }

        /// <summary>
        /// Phương thức khởi tạo mảng ô cờ
        /// </summary>
        public void ofChessArrInit()
        {
            for (int i = 0; i < _chessBoard.NumOfRow; i++)
            {
                for (int j = 0; j < _chessBoard.NumOfColumn; j++)
                {
                    _ofChessArr[i, j] = new OfChess(i, j, new Point(j * OfChess._width, i * OfChess._height), 0);
                }
            }
        }

        /// <summary>
        /// Phương thức: đánh cờ
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="gra"></param>
        /// <returns></returns>
        public bool check(int x, int y, Graphics gra)
        {
            // Không cho vẽ ở trên các đường biên
            if (x % OfChess._width == 0 || y % OfChess._height == 0)
                return false;

            int _col = x / OfChess._width;
            int _row = y / OfChess._height;

            if (_ofChessArr[_row, _col].Selected != 0) // Nếu đánh đánh ô đó rồi thì không cho đánh lại
                return false;

            switch (_turn)
            {
                case 1:
                    {
                        _ofChessArr[_row, _col].Selected = 1;
                        _chessBoard.drawChessman(gra, _ofChessArr[_row, _col].Position, _solidRed);
                        _turn = 2;
                        break;
                    }
                case 2:
                    {
                        _ofChessArr[_row, _col].Selected = 2;
                        _chessBoard.drawChessman(gra, _ofChessArr[_row, _col].Position, _solidBlue);
                        _turn = 1;
                        break;
                    }
                default:
                    MessageBox.Show("Có lỗi xảy ra!");
                    break;

            }

            _undoStack = new Stack<OfChess>();
            OfChess _temp = new OfChess(_ofChessArr[_row, _col].Row, _ofChessArr[_row, _col].Column, _ofChessArr[_row, _col].Position, _ofChessArr[_row, _col].Selected);
            _checkedStack.Push(_temp);
            return true;
        }

        /// <summary>
        /// Vẽ lại bàn cờ khi minimize
        /// </summary>
        /// <param name="gra"></param>
        public void drawChessmanAgain(Graphics gra)
        {
            foreach (OfChess ofChess in _checkedStack)
            {
                if (ofChess.Selected == 1)
                {
                    _chessBoard.drawChessman(gra, ofChess.Position, _solidRed);
                }
                else if (ofChess.Selected == 2)
                {
                    _chessBoard.drawChessman(gra, ofChess.Position, _solidBlue);
                }
            }
        }

        /// <summary>
        /// Khởi tạo bàn cờ, bắt đầu chơi chế độ PvsC
        /// </summary>
        /// <param name="gra"></param>
        public void StartPvsC(Graphics gra)
        {
            _checkedStack = new Stack<OfChess>();
            _undoStack = new Stack<OfChess>();
            _ready = true;
            ofChessArrInit();
            DrawChessBoard(gra);
            _turn = 1;
            Mode = 1;
            ComputerInit(gra);
        }

        /// <summary>
        /// Khởi tạo bàn cờ, bắt đầu chơi chế độ PvsP
        /// </summary>
        /// <param name="gra"></param>
        public void StartPvsP(Graphics gra)
        {
            _checkedStack = new Stack<OfChess>();
            _undoStack = new Stack<OfChess>();
            _ready = true;
            ofChessArrInit();
            DrawChessBoard(gra);
            _turn = 1;
            Mode = 2;
        }

    #region Phương thức Undo
        /// <summary>
        /// Phương thức Undo: Ctrl + Z
        /// </summary>
        /// <param name="gra"></param>
        public void Undo(Graphics gra)
        {
            if (_checkedStack.Count != 0)
            {
                OfChess _ofChessTemp = _checkedStack.Pop();
                _undoStack.Push(new OfChess(_ofChessTemp.Row, _ofChessTemp.Column, _ofChessTemp.Position, _ofChessTemp.Selected));
                _ofChessArr[_ofChessTemp.Row, _ofChessTemp.Column].Selected = 0;
                _chessBoard.deleteChessman(gra, _ofChessTemp.Position, _solidChessBoard);

                if (_turn == 1)
                    _turn = 2;
                else
                    _turn = 1;
            }
        }
        #endregion

    #region Phương thức Redo
        /// <summary>
        /// Phương thức Redo: Ctrl + Y
        /// </summary>
        /// <param name="gra"></param>
        public void Redo(Graphics gra)
        {
            if (_undoStack.Count != 0)
            {
                OfChess _ofChessTemp = _undoStack.Pop();
                _checkedStack.Push(new OfChess(_ofChessTemp.Row, _ofChessTemp.Column, _ofChessTemp.Position, _ofChessTemp.Selected));
                _ofChessArr[_ofChessTemp.Row, _ofChessTemp.Column].Selected = _ofChessTemp.Selected;
                _chessBoard.drawChessman(gra, _ofChessTemp.Position, _ofChessTemp.Selected == 1 ? _solidRed : _solidBlue);

                if (_turn == 2)
                    _turn = 1;
                else
                    _turn = 2;
            }
        }
        #endregion

    #region Phương thức kiểm tra kết thúc game
        /// <summary>
        /// Phương thức: kiểm tra trận đấu đã kết thúc hay chưa
        /// </summary>
        /// <returns></returns>
        public bool CheckForTheWin()
        {
            // TH1: Hòa cờ
            if (_checkedStack.Count == _chessBoard.NumOfColumn * _chessBoard.NumOfRow)
            {
                _result = Result.Draw; // Hòa cờ
                return true;
            }

            foreach (OfChess _of in _checkedStack)
            {
                if (CheckedByColumn(_of.Row, _of.Column, _of.Selected) || CheckedByRow(_of.Row, _of.Column, _of.Selected) || CheckedByBackSlash(_of.Row, _of.Column, _of.Selected) || CheckedBySlash(_of.Row, _of.Column, _of.Selected))
                {
                    _result = _of.Selected == 1 ? Result.Player1 : Result.Player2;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Phương thức: màn hình endgame
        /// </summary>
        public void EndGame()
        {
            switch (_result)
            {
                case Result.Draw:
                    {
                        MessageBox.Show("Hòa cờ!");
                        break;
                    }
                case Result.Player1:
                    {
                        MessageBox.Show("Người chơi 1 chiến thắng!");
                        break;
                    }
                case Result.Player2:
                    {
                        MessageBox.Show("Người chơi 2 chiến thắng!");
                        break;
                    }
                case Result.Com:
                    {
                        MessageBox.Show("Máy thắng! You close :)))");
                        break;
                    }

            }
            _ready = false;
        }

        // Kiểm tra theo chiều dọc đủ điều kiện chiến thắng hay chưa
        private bool CheckedByColumn(int currRow, int currCol, int currSelected)
        {
            int _count; // Đếm số quân cờ
            if (currRow > _chessBoard.NumOfRow - 5) // Nếu dòng hiện tại là dòng lớn hơn 15 thì ko cần xét
                return false;

            // Kiểm tra biên trên, biên dưới theo chiều dọc
            for (_count = 1; _count < 5; _count++)
            {
                if (_ofChessArr[currRow + _count, currCol].Selected != currSelected)
                    return false;
            }
            if (currRow == 0 || currRow + _count == _chessBoard.NumOfRow)
                return true;

            //
            // Kiểm tra các quân cờ nằm ở giữa bàn cờ
            //
            if (_ofChessArr[currRow - 1, currCol].Selected == 0 || _ofChessArr[currRow + _count, currCol].Selected == 0) // Kiểm tra nếu không có bị chặn ờ 2 đầu thì chiến thắng
            {
                return true;
            }

            return false;
        }

        // Kiểm tra theo chiều ngang đủ điều kiện chiến thắng hay chưa
        private bool CheckedByRow(int currRow, int currCol, int currSelected)
        {
            int _count; // Đếm số quân cờ
            if (currCol > _chessBoard.NumOfColumn - 5) // Nếu cột hiện tại là dòng lớn hơn 15 thì ko cần xét
                return false;

            // Kiểm tra biên trên, biên dưới theo chiều dọc
            for (_count = 1; _count < 5; _count++)
            {
                if (_ofChessArr[currRow, currCol + _count].Selected != currSelected)
                    return false;
            }
            if (currCol == 0 || currCol + _count == _chessBoard.NumOfColumn)
                return true;

            //
            // Kiểm tra các quân cờ nằm ở giữa bàn cờ
            //
            if (_ofChessArr[currRow, currCol - 1].Selected == 0 || _ofChessArr[currRow, currCol + _count].Selected == 0) // Kiểm tra nếu không có bị chặn ờ 2 đầu thì chiến thắng
            {
                return true;
            }

            return false;
        }

        // Kiểm tra theo chiều chéo xuôi (/) xem đủ điều kiện chiến thắng hay chưa (Duyệt từ dưới lên trên)
        private bool CheckedBySlash(int currRow, int currCol, int currSelected)
        {
            int _count; // Đếm số quân cờ

            if (currRow < 4 || currCol > _chessBoard.NumOfColumn - 5)
                return false;

            for (_count = 1; _count < 5; _count++)
            {
                if (_ofChessArr[currRow - _count, currCol + _count].Selected != currSelected)
                    return false;
            }

            if (currRow == 4 || currRow + _count == _chessBoard.NumOfRow - 1 || currCol == 0 || currCol + _count == _chessBoard.NumOfColumn)
                return true;

            
            if (_ofChessArr[currRow + 1, currCol - 1].Selected == 0 || _ofChessArr[currRow - _count, currCol + _count].Selected == 0) // Kiểm tra nếu không có bị chặn ờ 2 đầu thì chiến thắng
            {
                return true;
            }

            return false;
        }

        // Kiểm tra theo chiều chéo ngược (\) xem đủ điều kiện chiến thắng hay chưa
        private bool CheckedByBackSlash(int currRow, int currCol, int currSelected)
        {
            int _count; // Đếm số quân cờ
            if (currRow > _chessBoard.NumOfRow - 5 || currCol > _chessBoard.NumOfColumn - 5) // kết hợp duyệt dọc và duyệt ngang
                return false;

            
            for (_count = 1; _count < 5; _count++)
            {
                if (_ofChessArr[currRow + _count, currCol + _count].Selected != currSelected)
                    return false;
            }
            if (currRow == 0 || currRow + _count == _chessBoard.NumOfRow || currCol == 0 || currCol + _count == _chessBoard.NumOfColumn)
                return true;

            //
            // Kiểm tra các quân cờ nằm ở giữa bàn cờ
            //
            if (_ofChessArr[currRow - 1, currCol - 1].Selected == 0 || _ofChessArr[currRow + _count, currCol + _count].Selected == 0) // Kiểm tra nếu không có bị chặn ờ 2 đầu thì chiến thắng
            {
                return true;
            }

            return false;
        }
        #endregion

    #region Phương thức: Trí tuệ nhân tạo trong game - AI
        private long[] _acttackArr = new long[7] { 0, 9, 54, 162, 1458, 13112, 118008 };
        private long[] _defenseArr = new long[7] { 0, 3, 27, 99, 729, 6561, 59049 };

        // Khởi tạo máy - chế độ chơi với máy
        public void ComputerInit(Graphics gra)
        {
            if (_checkedStack.Count == 0) // Nếu là bàn cờ trống, sẽ đánh ở giữa
            {
                check(_chessBoard.NumOfColumn / 2 * OfChess._width + 1, _chessBoard.NumOfRow / 2 * OfChess._height + 1, gra);
            }
            else
            {
                OfChess _of = LookingMoves();
                check(_of.Position.X + 1, _of.Position.Y + 1, gra);
            }
        }

        // Tìm nước đi tốt nhất nhờ thuật toán
        private OfChess LookingMoves()
        {
            OfChess _of = new OfChess();
            long _maxScore = 0;

            //
            // Phương thức vét cạn
            //
            for (int i=0; i < _chessBoard.NumOfRow; i++)
            {
                for (int j=0; j < _chessBoard.NumOfColumn; j++)
                {
                    if (_ofChessArr[i, j].Selected == 0)
                    {
                        long _acttackScore = AttackScoreByColumn(i, j) + AttackScoreByRow(i, j) + AttackScoreByBackSlash(i, j) + AttackScoreBySlash(i, j);
                        long _defenseScore = DefenseScoreByColumn(i, j) + DefenseScoreByRow(i, j) + DefenseScoreByBackSlash(i, j) + DefenseScoreBySlash(i, j);
                        long _temp = _acttackScore > _defenseScore ? _acttackScore : _defenseScore;

                        if (_maxScore < _temp)
                        {
                            _maxScore = _temp;
                            _of = new OfChess(_ofChessArr[i, j].Row, _ofChessArr[i, j].Column, _ofChessArr[i, j].Position, _ofChessArr[i, j].Selected);
                        }

                    }
                }
            }

            return _of;
        }

        #region Tính điểm tấn công
                // Điểm tấn công duyệt theo chiều dọc
                private long AttackScoreByColumn(int currRow, int currCol)
                {
                    long _s = 0;                // Điểm tổng
                    int _troopNum = 0;          // Số quân ta
                    int _enemyNum = 0;          // Số quân địch

                    // Vòng lặp duyệt từ trên xuống dưới
                    for (int count = 1; count < 6 && currRow + count < _chessBoard.NumOfRow; count++)
                    {
                        if (_ofChessArr[currRow + count, currCol].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow + count, currCol].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break; 
                    }

                    // Vòng lặp duyệt từ dưới lên trên
                    for (int count = 1; count < 6 && currRow - count >= 0; count++)
                    {
                        if (_ofChessArr[currRow - count, currCol].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow - count, currCol].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break;
                    }

                    // Nếu số quân địch = 2, thì nước đó đã bị chặn 2 đầu và số quân địch tối đa cùng là 2
                    if (_enemyNum == 2)
                        return 0;

                    _s -= _defenseArr[_enemyNum + 1] * 2; //heuristic

                    _s += _acttackArr[_troopNum];

                    return _s;
                }

                // Điểm tấn công duyệt theo chiều ngang
                private long AttackScoreByRow(int currRow, int currCol)
                {
                    long _s = 0;                // Điểm tổng
                    int _troopNum = 0;          // Số quân ta
                    int _enemyNum = 0;          // Số quân địch

                    // Vòng lặp duyệt từ trái sang phải
                    for (int count = 1; count < 6 && currCol + count < _chessBoard.NumOfColumn; count++)
                    {
                        if (_ofChessArr[currRow, currCol + count].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow, currCol + count].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break;
                    }

                    // Vòng lặp duyệt từ phải sang trái
                    for (int count = 1; count < 6 && currCol - count >= 0; count++)
                    {
                        if (_ofChessArr[currRow, currCol - count].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow, currCol - count].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break;
                    }

                    // Nếu số quân địch = 2, thì nước đó đã bị chặn 2 đầu và số quân địch tối đa cùng là 2
                    if (_enemyNum == 2)
                        return 0;

                    _s -= _defenseArr[_enemyNum + 1] * 2; //heuristic

                    _s += _acttackArr[_troopNum];

                    return _s;
                }

                // Điểm tấn công duyệt theo chiều chéo ngược (/)
                private long AttackScoreByBackSlash(int currRow, int currCol)
                {
                    long _s = 0;                // Điểm tổng
                    int _troopNum = 0;          // Số quân ta
                    int _enemyNum = 0;          // Số quân địch

            
                    for (int count = 1; count < 6 && currCol + count < _chessBoard.NumOfColumn && currRow - count >= 0; count++)
                    {
                        if (_ofChessArr[currRow - count, currCol + count].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow - count, currCol + count].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break;
                    }

            
                    for (int count = 1; count < 6 && currCol - count >= 0 && currRow + count < _chessBoard.NumOfRow; count++)
                    {
                        if (_ofChessArr[currRow + count, currCol - count].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow + count, currCol - count].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break;
                    }

                    // Nếu số quân địch = 2, thì nước đó đã bị chặn 2 đầu và số quân địch tối đa cùng là 2
                    if (_enemyNum == 2)
                        return 0;

                    _s -= _defenseArr[_enemyNum + 1] * 2; //heuristic

                    _s += _acttackArr[_troopNum];

                    return _s;
                }

                // Điểm tấn công duyệt theo chiều chéo xuôi (\)
                private long AttackScoreBySlash(int currRow, int currCol)
                {
                    long _s = 0;                // Điểm tổng
                    int _troopNum = 0;          // Số quân ta
                    int _enemyNum = 0;          // Số quân địch

                    // Từ trái trên xuống phải dưới
                    for (int count = 1; count < 6 && currCol + count < _chessBoard.NumOfColumn && currRow + count < _chessBoard.NumOfRow; count++)
                    {
                        if (_ofChessArr[currRow + count, currCol + count].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow + count, currCol + count].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break;
                    }

                    // Từ góc phải dưới lên góc trái trên
                    for (int count = 1; count < 6 && currCol - count >= 0 && currRow - count >= 0; count++)
                    {
                        if (_ofChessArr[currRow - count, currCol - count].Selected == 1)
                        {
                            _troopNum++;
                        }
                        else if (_ofChessArr[currRow - count, currCol - count].Selected == 2)
                        {
                            _enemyNum++;
                            break; // Đã bị chặn, thoát vòng lặp
                        }
                        else // Ô trống
                            break;
                    }

                    // Nếu số quân địch = 2, thì nước đó đã bị chặn 2 đầu và số quân địch tối đa cùng là 2
                    if (_enemyNum == 2)
                        return 0;

                    _s -= _defenseArr[_enemyNum + 1] * 2; //heuristic

                    _s += _acttackArr[_troopNum];

                    return _s;
                }

        #endregion

        #region Tính điểm phòng ngự
        // Điểm phòng ngự duyệt theo chiều dọc
        private long DefenseScoreByColumn(int currRow, int currCol)
        {
            long _s = 0;                // Điểm tổng
            int _troopNum = 0;          // Số quân ta
            int _enemyNum = 0;          // Số quân địch

            // Vòng lặp duyệt từ trên xuống dưới
            for (int count = 1; count < 6 && currRow + count < _chessBoard.NumOfRow; count++)
            {
                if (_ofChessArr[currRow + count, currCol].Selected == 1) // Gặp quân ta
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow + count, currCol].Selected == 2) // Gặp quân địch
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }

            // Vòng lặp duyệt từ dưới lên trên
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if (_ofChessArr[currRow - count, currCol].Selected == 1)
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow - count, currCol].Selected == 2)
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }

            if (_troopNum == 2)
                return 0;

            _s += _defenseArr[_enemyNum]; //heuristic

            return _s;
        }

        // Điểm phòng ngự duyệt theo chiều ngang
        private long DefenseScoreByRow(int currRow, int currCol)
        {
            long _s = 0;                // Điểm tổng
            int _troopNum = 0;          // Số quân ta
            int _enemyNum = 0;          // Số quân địch

            // Vòng lặp duyệt từ trái sang phải
            for (int count = 1; count < 6 && currCol + count < _chessBoard.NumOfColumn; count++)
            {
                if (_ofChessArr[currRow, currCol + count].Selected == 1)
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow, currCol + count].Selected == 2)
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }

            // Vòng lặp duyệt từ phải sang trái
            for (int count = 1; count < 6 && currCol - count >= 0; count++)
            {
                if (_ofChessArr[currRow, currCol - count].Selected == 1)
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow, currCol - count].Selected == 2)
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }

            if (_troopNum == 2)
                return 0;

            _s += _defenseArr[_enemyNum]; //heuristic

            return _s;
        }

        // Điểm phòng ngự duyệt theo chiều chéo ngược (/)
        private long DefenseScoreByBackSlash(int currRow, int currCol)
        {
            long _s = 0;                // Điểm tổng
            int _troopNum = 0;          // Số quân ta
            int _enemyNum = 0;          // Số quân địch


            for (int count = 1; count < 6 && currCol + count < _chessBoard.NumOfColumn && currRow - count >= 0; count++)
            {
                if (_ofChessArr[currRow - count, currCol + count].Selected == 1)
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow - count, currCol + count].Selected == 2)
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }


            for (int count = 1; count < 6 && currCol - count >= 0 && currRow + count < _chessBoard.NumOfRow; count++)
            {
                if (_ofChessArr[currRow + count, currCol - count].Selected == 1)
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow + count, currCol - count].Selected == 2)
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }

            if (_troopNum == 2)
                return 0;

            _s += _defenseArr[_troopNum]; //heuristic

            return _s;
        }

        // Điểm tấn phòng ngự theo chiều chéo xuôi (\)
        private long DefenseScoreBySlash(int currRow, int currCol)
        {
            long _s = 0;                // Điểm tổng
            int _troopNum = 0;          // Số quân ta
            int _enemyNum = 0;          // Số quân địch

            // Từ trái trên xuống phải dưới
            for (int count = 1; count < 6 && currCol + count < _chessBoard.NumOfColumn && currRow + count < _chessBoard.NumOfRow; count++)
            {
                if (_ofChessArr[currRow + count, currCol + count].Selected == 1)
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow + count, currCol + count].Selected == 2)
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }

            // Từ góc phải dưới lên góc trái trên
            for (int count = 1; count < 6 && currCol - count >= 0 && currRow - count >= 0; count++)
            {
                if (_ofChessArr[currRow - count, currCol - count].Selected == 1)
                {
                    _troopNum++;
                    break;
                }
                else if (_ofChessArr[currRow - count, currCol - count].Selected == 2)
                {
                    _enemyNum++;
                }
                else // Ô trống
                    break;
            }

            
            if (_troopNum == 2)
                return 0;

            _s += _defenseArr[_troopNum]; //heuristic

            return _s;
        }

        #endregion


        #endregion


    }
}
