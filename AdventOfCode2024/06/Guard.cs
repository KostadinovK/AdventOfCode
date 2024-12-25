using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06
{
    public class Coordinate
    {
        public Coordinate(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }

    public class Guard
    {
        public Coordinate Position { get; set; }

        public Coordinate Direction { get; set; }

        public char Arrow { get; set; } = '^';

        public Guard(int posRow, int posCol, int dirRow, int dirCol)
        {
            Position = new Coordinate(posRow, posCol);
            Direction = new Coordinate(dirRow, dirCol);
        }

        public void ChangeArrow()
        {
            switch (Arrow)
            {
                case '^':
                    Arrow = '>';
                    break;
                case '>':
                    Arrow = 'v';
                    break;
                case 'v':
                    Arrow = '<';
                    break;
                case '<':
                    Arrow = '^';
                    break;
            }
        }

        public void SetDirectionFromArrow()
        {
            switch (Arrow)
            {
                case '^':
                    Direction = new Coordinate(-1, 0);
                    break;
                case '>':
                    Direction = new Coordinate(0, 1);
                    break;
                case 'v':
                    Direction = new Coordinate(1, 0);
                    break;
                case '<':
                    Direction = new Coordinate(0, -1);
                    break;
            }
        }
    }
}
