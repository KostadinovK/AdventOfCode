using _04_12;

var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var numbers = lines[0].Split(',').Select(x => int.Parse(x)).ToList();

var boards = InitializeBingoBoards(lines);

var bingoBoard = new BingoNumber[5,5];
var winningNumber = -1;

foreach (var number in numbers)
{
    foreach (var board in boards)
    {
        var hasBingo = false;
        for (int row = 0; row < board.GetLength(0); row++)
        {
            var hasRowBingo = true;
            var hasColBingo = true;
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (number == board[row, col].Number)
                {
                    board[row, col].IsMarked = true;
                }

                if (!board[row, col].IsMarked)
                {
                    hasRowBingo = false;
                }

                if (!board[col, row].IsMarked)
                {
                    hasColBingo = false;
                }
            }

            if (hasRowBingo || hasColBingo)
            {
                hasBingo = true;
                bingoBoard = board;
                winningNumber = number;
                break;
            }
        }

        if (hasBingo)
        {
            break;
        }
    }

    if (winningNumber != -1)
    {
        break;
    }
}

var bingoBoards = new List<BingoNumber[,]>();
var lastWinningNumber = -1;

foreach (var number in numbers)
{
    foreach (var board in boards.ToList())
    {
        var hasBingo = false;
        for (int row = 0; row < board.GetLength(0); row++)
        {
            var hasRowBingo = true;
            var hasColBingo = true;
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (number == board[row, col].Number)
                {
                    board[row, col].IsMarked = true;
                }

                if (!board[row, col].IsMarked)
                {
                    hasRowBingo = false;
                }

                if (!board[col, row].IsMarked)
                {
                    hasColBingo = false;
                }
            }

            if (hasRowBingo || hasColBingo)
            {
                hasBingo = true;
                bingoBoards.Add(board);
                boards.Remove(board);
                lastWinningNumber = number;
                break;
            }
        }
    }
}


var firstBingoBoardScore = GetBingoBoardScore(bingoBoard, winningNumber);
var lastBingoBoardScore = GetBingoBoardScore(bingoBoards[bingoBoards.Count - 1], lastWinningNumber);

Console.WriteLine("First Bingo Board score: " + firstBingoBoardScore);
Console.WriteLine("Last Bingo Board score: " + lastBingoBoardScore);


List<BingoNumber[,]> InitializeBingoBoards(string[] lines)
{
    var boards = new List<BingoNumber[,]>();

    for (var i = 2; i <= lines.Length - 5; i += 6)
    {
        var bingoBoard = new BingoNumber[5, 5];
        var rowIndex = 0;

        for (var j = i; j < i + 5; j++)
        {
            var line = lines[j];

            var rowNums = line
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => int.Parse(x))
                .ToList();

            for (var col = 0; col < bingoBoard.GetLength(0); col++)
            {
                bingoBoard[rowIndex, col] = new BingoNumber(rowNums[col]);
            }

            rowIndex++;
        }

        boards.Add(bingoBoard);
    }

    return boards;
}

int GetBingoBoardScore(BingoNumber[,] board, int winningNumber)
{
    var score = 0;

    for (int row = 0; row < board.GetLength(0); row++)
    {
        for (int col = 0; col < board.GetLength(1); col++)
        {
            if (!board[row, col].IsMarked)
            {
                score += board[row, col].Number;
            }
        }
    }

    return score * winningNumber;
}