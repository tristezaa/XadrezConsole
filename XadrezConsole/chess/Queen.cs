using System;
using System.Collections.Generic;
using System.Text;
using XadrezConsole.board;

namespace XadrezConsole.chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "Q";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.Piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];
            Position pos = new Position(0, 0);
            // up
            pos.SetValues(position.line - 1, position.column);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.line--;
            }
            //down
            pos.SetValues(position.line + 1, position.column);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.line++;
            }
            //right
            pos.SetValues(position.line, position.column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.column++;
            }
            //left
            pos.SetValues(position.line, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.column--;
            }
            // ne
            pos.SetValues(position.line - 1, position.column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.SetValues(pos.line - 1, pos.column + 1);
            }
            // se
            pos.SetValues(position.line + 1, position.column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.SetValues(pos.line + 1, pos.column + 1);
            }
            // sw
            pos.SetValues(position.line + 1, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.SetValues(pos.line + 1, pos.column - 1);
            }
            // nw
            pos.SetValues(position.line - 1, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.SetValues(pos.line - 1, pos.column - 1);
            }
            return mat;
        }
    }
}
