using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bobail
{
    public class Board
    {
        public const int Size = 5;
        public Piece[,] Grid { get; set; }

        public Board()
        {
            Grid = new Piece[Size, Size];
            Init();
        }

        private void Init()
        {
            // exemplu: primele 2 rânduri Player1, ultimele 2 Player2
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    Grid[r, c] = Piece.Empty;

            // player 1
            for (int c = 0; c < Size; c++)
                Grid[0, c] = Grid[1, c] = Piece.Player1;

            // player 2
            for (int c = 0; c < Size; c++)
                Grid[3, c] = Grid[4, c] = Piece.Player2;
        }

        public void Print()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                    Console.Write(Grid[r, c] switch
                    {
                        Piece.Empty => ". ",
                        Piece.Player1 => "1 ",
                        Piece.Player2 => "2 ",
                        _ => "? "
                    });
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

}
