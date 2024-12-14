var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var board = new char[lines.Length, lines[0].Length];

for (int col = 0; col < lines.Length; col++)
{
    for (int row = 0; row < lines[col].Length; row++)
    {
        board[col, row] = lines[col][row];
    }
}


GetXmasWordsCount(board);
GetMasXesCount(board);

void GetMasXesCount(char[,] board)
{
    var masXesCount = 0;

    var boardRows = board.GetLength(0);
    var boardCols = board.GetLength(1);
    for (int row = 0; row < boardRows; row++)
    {
        for (int col = 0; col < boardCols; col++)
        {
            if (board[row, col] == 'A')
            {
                var isMasXes = IsMasXes(board, row, col);

                if (isMasXes)
                {
                    masXesCount++;
                }
            }
        }
    }

    Console.WriteLine($"MAS Xes count: {masXesCount}");
}

bool IsMasXes(char[,] board, int row, int col)
{
    var lastBoardRowIndex = board.GetLength(0) - 1;
    var lastBoardColIndex = board.GetLength(1) - 1;

    
    if (row > 0 && col > 0 && row < lastBoardRowIndex && col < lastBoardColIndex)
    {
        //up-left down-right diagonal check
        var upLeftDiag = board[row - 1, col - 1];
        var downRightDiag = board[row + 1, col + 1];

        if ((upLeftDiag != 'M' && upLeftDiag != 'S') || (downRightDiag != 'M' && downRightDiag != 'S') || downRightDiag == upLeftDiag)
        {
            return false;
        }

        //up-right down-left diagonal check
        var upRightDiag = board[row - 1, col + 1];
        var downLeftDiag = board[row + 1, col - 1];

        if ((upRightDiag != 'M' && upRightDiag != 'S') || (downLeftDiag != 'M' && downLeftDiag != 'S') || upRightDiag == downLeftDiag)
        {
            return false;
        }

        return true;
    }

    return false;
}

void GetXmasWordsCount(char[,] board)
{
    var xmasCount = 0;

    var boardRows = board.GetLength(0);
    var boardCols = board.GetLength(1);
    for (int row = 0; row < boardRows; row++)
    {
        for (int col = 0; col < boardCols; col++)
        {
            if (board[row, col] == 'X')
            {
                var letterMs = GetLetterNeighbour(board, row, col, 'M');

                for (int i = 0; i < letterMs.Count; i++)
                {
                    var mrow = letterMs[i].row;
                    var mcol = letterMs[i].col;

                    var rowDiff = mrow - row;
                    var colDiff = mcol - col;

                    var lastRowIndex = mrow + rowDiff * 2;
                    var lastColIndex = mcol + colDiff * 2;

                    if (lastRowIndex >= 0 && lastRowIndex < boardRows && lastColIndex >= 0 &&
                        lastColIndex < boardCols && board[mrow + rowDiff, mcol + colDiff] == 'A')
                    {
                        if (board[lastRowIndex, lastColIndex] == 'S')
                        {
                            xmasCount++;
                        }
                    }
                }
            }
        }
    }

    Console.WriteLine($"XMAS count: {xmasCount}");
}

List<(int row, int col)> GetLetterNeighbour(char[,] board, int row, int col, char letter)
{
        var lastBoardRowIndex = board.GetLength(0) - 1;
        var lastBoardColIndex = board.GetLength(1) - 1;

        var neighbours = new List<(int row, int col)>();

        //up
        if (row > 0 && board[row - 1, col] == letter)
        {
            neighbours.Add((row - 1, col));
        }

        //down
        if (row < lastBoardRowIndex && board[row+1, col] == letter)
        {
            neighbours.Add((row + 1, col));
        }

        //left
        if (col > 0 && board[row, col - 1] == letter)
        {
            neighbours.Add((row, col - 1));
        }

        //right
        if (col < lastBoardColIndex && board[row, col + 1] == letter)
        {
            neighbours.Add((row, col + 1));
        }

        //up-left diagonal
        if (row > 0 && col > 0 && board[row - 1, col - 1] == letter)
        {
            neighbours.Add((row - 1, col - 1));
        }

        //up-right diagonal
        if (row > 0 && col < lastBoardColIndex && board[row - 1, col + 1] == letter)
        {
            neighbours.Add((row - 1, col + 1));
        }

        //down-right diagonal
        if (row < lastBoardRowIndex && col < lastBoardColIndex && board[row + 1, col + 1] == letter)
        {
            neighbours.Add((row + 1, col + 1));
        }

        //down-left diagonal
        if (row < lastBoardRowIndex && col > 0 && board[row + 1, col - 1] == letter)
        {
            neighbours.Add((row + 1, col - 1));
        }

        return neighbours;
}
