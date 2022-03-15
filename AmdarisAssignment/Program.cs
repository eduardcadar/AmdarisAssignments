using System;
using System.Collections;
using System.Collections.Generic;

namespace AmdarisAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPiece pawn = new Pawn(true);
            pawn.Square = new Square(1, 2);
            ChessPiece rook = new Rook(true);
            rook.Square = new Square(2, 3);
            ChessPiece bishop = new Bishop(false);
            bishop.Square = new Square(1, 4);
            ChessPiece queen = new Queen(true);
            queen.Square = new Square(5, 7);
            ChessPiece king = new King(false);
            king.Square = new Square(2, 1);
            ChessPiece knight = new Knight(false);
            knight.Square = new Square(4, 3);
            ChessPiece pawn2 = new Pawn(false);
            pawn2.Square = new Square(8, 3);
            List<ChessPiece> pieces = new();

            pieces.Add(pawn);
            pieces.Add(rook);
            pieces.Add(bishop);
            pieces.Add(queen);
            pieces.Add(king);
            pieces.Add(knight);
            pieces.Add(pawn2);

            ChessBoard board = new(pieces);

            foreach (ChessPiece piece in board)
            {
                Console.WriteLine(piece);
                piece.Move();
            }

            Console.WriteLine();
            Console.WriteLine(pawn2);
            pawn2.Square = (Square)pawn.Square.Clone();
            board.RemovePiece(pawn);
            Console.WriteLine(pawn2);
            Console.WriteLine();

            foreach (ChessPiece piece in board)
            {
                Console.WriteLine(piece);
                piece.Move();
            }
        }
    }

    public class ChessBoard : IEnumerable<ChessPiece>
    {
        private readonly List<ChessPiece> pieces;
        public ChessBoard(List<ChessPiece> pieces)
        {
            this.pieces = pieces;
        }
        public void RemovePiece(ChessPiece piece)
        {
            this.pieces.Remove(piece);
        }
        public IEnumerator<ChessPiece> GetEnumerator()
        {
            return pieces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class ChessPiece
    {
        public int Value { get; set; }
        public bool IsWhite { get; }
        public Square Square { get; set; }
        public ChessPiece(bool isWhite)
        {
            this.IsWhite = isWhite;
        }
        public abstract void Move();
        public void Move(Square square)
        {
            this.Square = square;
        }
        public abstract override string ToString();
    }

    public class Square : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Square(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public object Clone()
        {
            return new Square(this.X, this.Y);
        }

        public override string ToString()
        {
            return "(" + this.X + ", " + this.Y + ")";
        }
    }

    public class King : ChessPiece
    {
        public King(bool isWhite) : base(isWhite)
        {
            this.Value = 1000;
        }

        public override void Move()
        {
            Console.WriteLine("Move to any adjacent square");
        }

        public override string ToString()
        {
            return "King on " + this.Square;
        }
    }

    public class Queen : ChessPiece
    {
        public Queen(bool isWhite) : base(isWhite)
        {
            this.Value = 9;
        }

        public override void Move()
        {
            Console.WriteLine("Move any number of squares in a straight line or diagonally");
        }

        public override string ToString()
        {
            return "Queen on " + this.Square;
        }
    }

    public class Rook : ChessPiece
    {
        public Rook(bool isWhite) : base(isWhite)
        {
            this.Value = 5;
        }

        public override void Move()
        {
            Console.WriteLine("Move any number of squares in a straight line");
        }

        public override string ToString()
        {
            return "Rook on " + this.Square;
        }
    }

    public class Bishop : ChessPiece
    {
        public Bishop(bool isWhite) : base(isWhite)
        {
            this.Value = 3;
        }

        public override void Move()
        {
            Console.WriteLine("Move any number of squares diagonally");
        }

        public override string ToString()
        {
            return "Bishop on " + this.Square;
        }
    }

    public class Knight : ChessPiece
    {
        public Knight(bool isWhite) : base(isWhite)
        {
            this.Value = 3;
        }

        public override void Move()
        {
            Console.WriteLine("Move in L shape, two squares in a straight line then one to the side");
        }

        public override string ToString()
        {
            return "Knight on " + this.Square;
        }
    }

    public class Pawn : ChessPiece
    {
        public Pawn(bool isWhite) : base(isWhite)
        {
            this.Value = 1;
        }

        public override void Move()
        {
            Console.WriteLine("Move one square forward");
        }

        public override string ToString()
        {
            return "Pawn on " + this.Square;
        }
    }
}
