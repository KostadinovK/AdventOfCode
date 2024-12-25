using _06;
using System.ComponentModel;

var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\sample.txt");

var board = new char[lines.Length, lines[0].Length];
Guard guard = new Guard(0, 0, 0, 0);

for (int row = 0; row < lines.Length; row++)
{
    for (int col = 0; col < lines[row].Length; col++)
    {
        board[row, col] = lines[row][col];

        if (board[row, col] == '^')
        {
            guard = new Guard(row, col, -1, 0);
        }
    }
}

Part2(guard, board);

void Part1(Guard guard, char[,] board)
{
    while (true)
    {
        var newRow = guard.Position.Row + guard.Direction.Row;
        var newCol = guard.Position.Col + guard.Direction.Col;

        var isOutside = !IsInBounds(newRow, newCol, board);
        if (isOutside)
        {
            board[guard.Position.Row, guard.Position.Col] = 'X';
            break;
        }

        ProcessMove(board, guard);
    }

    PrintBoard(board);
    PrintMoveCounter(board);
}

void Part2(Guard part2Guard, char[,] part2Board)
{
    var initialGuard = new Guard(part2Guard.Position.Row, part2Guard.Position.Col, part2Guard.Direction.Row, part2Guard.Direction.Col);
    var visitedFields = new HashSet<(int, int)>();
    while (true)
    {
        visitedFields.Add((part2Guard.Position.Row, part2Guard.Position.Col));

        var newRow = part2Guard.Position.Row + part2Guard.Direction.Row;
        var newCol = part2Guard.Position.Col + part2Guard.Direction.Col;

        var isOutside = !IsInBounds(newRow, newCol, part2Board);
        if (isOutside)
        {
            part2Board[part2Guard.Position.Row, part2Guard.Position.Col] = 'X';
            break;
        }

        ProcessMove(part2Board, part2Guard);
    }

    var totalWays = 0;
    foreach (var (row, col) in visitedFields)
    {
        if (row == 6 && col == 3)
        {
            var test = 1;
        }
        part2Board[row, col] = '#';

        if (IsAPatrolLoop(initialGuard, part2Board))
        {
            totalWays++;
        }

        part2Board[row, col] = '.';
    }

    PrintBoard(part2Board);
    Console.WriteLine($"Total Ways: {totalWays}");
}

bool IsAPatrolLoop(Guard g, char[,] b)
{
    var visitedFields = new HashSet<(int, int, int, int)>();
    while (true)
    {
        visitedFields.Add((g.Position.Row, g.Position.Col, g.Direction.Row, g.Direction.Col));

        var newRow = g.Position.Row + g.Direction.Row;
        var newCol = g.Position.Col + g.Direction.Col;

        var isOutside = !IsInBounds(newRow, newCol, b);
        if (isOutside)
        {
            break;
        }

        ProcessMove(b, g);

        if (visitedFields.Contains((g.Position.Row, g.Position.Col, g.Direction.Row, g.Direction.Col)))
        {
            return true;
        }
    }

    return false;
}

bool IsInBounds(int row, int col, char[,] board)
{
    return row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1);
}

void ProcessMove(char[,] bo, Guard gu)
{
    var nextPosition = new Coordinate(gu.Position.Row + gu.Direction.Row, gu.Position.Col + gu.Direction.Col);
    var boardSymbol = bo[nextPosition.Row, nextPosition.Col];

    if (boardSymbol == '#')
    {
        gu.ChangeArrow();
        gu.SetDirectionFromArrow();
    }
    else
    {
        bo[gu.Position.Row, gu.Position.Col] = 'X';
        gu.Position = nextPosition;
    }
}

void PrintBoard(char[,] board)
{
    for (int row = 0; row < board.GetLength(0); row++)
    {
        for (int col = 0; col < board.GetLength(1); col++)
        {
            Console.Write(board[row, col]);
        }

        Console.WriteLine();
    }
}

void PrintMoveCounter(char[,] board)
{
    var counter = 0;
    for (int row = 0; row < board.GetLength(0); row++)
    {
        for (int col = 0; col < board.GetLength(1); col++)
        {
            if (board[row, col] == 'X')
            {
                counter++;
            }
        }
    }

    Console.WriteLine($"Moves: {counter}");
}