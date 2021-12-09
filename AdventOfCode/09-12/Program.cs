using System.Data;

var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var heightmap = new int[lines.Length, lines[0].Length];

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        heightmap[i, j] = lines[i][j] - '0';
    }
}

var sum = 0;

for (int row = 0; row < heightmap.GetLength(0); row++)
{
    for (int col = 0; col < heightmap.GetLength(1); col++)
    {
        var curr = heightmap[row, col];
        
        if (col > 0 && heightmap[row, col - 1] <= curr)
        {
            continue;
        }

        if (col < heightmap.GetLength(1) - 1 && heightmap[row, col + 1] <= curr)
        {
            continue;
        }

        if (row > 0 && heightmap[row - 1, col] <= curr)
        {
            continue;
        }

        if (row < heightmap.GetLength(0) - 1 && heightmap[row + 1, col] <= curr)
        {
            continue;
        }

        sum += curr + 1;
    }
}

Console.WriteLine(sum);

var lowPoints = new List<(int row, int col)>();

for (int row = 0; row < heightmap.GetLength(0); row++)
{
    for (int col = 0; col < heightmap.GetLength(1); col++)
    {
        var curr = heightmap[row, col];

        if (col > 0 && heightmap[row, col - 1] <= curr)
        {
            continue;
        }

        if (col < heightmap.GetLength(1) - 1 && heightmap[row, col + 1] <= curr)
        {
            continue;
        }

        if (row > 0 && heightmap[row - 1, col] <= curr)
        {
            continue;
        }

        if (row < heightmap.GetLength(0) - 1 && heightmap[row + 1, col] <= curr)
        {
            continue;
        }

        lowPoints.Add((row, col));
    }
}

var basinSizes = new List<int>();
bool[,] boolHeightMapVisited = new bool[heightmap.GetLength(0), heightmap.GetLength(1)];

foreach (var lowPoint in lowPoints)
{
    var basinSize = GetBasinSize(lowPoint,1, heightmap, boolHeightMapVisited);

    basinSizes.Add(basinSize);
}

var res = basinSizes.OrderByDescending(x => x).Take(3).Aggregate(1, (a, b) => a * b);

Console.WriteLine(res);

int GetBasinSize((int row, int col) point, int basinSize, int[,] heightmap, bool[,] boolHeightMapVisited)
{
    var row = point.row;
    var col = point.col;

    boolHeightMapVisited[point.row, point.col] = true;

    if (point.row > 0 && heightmap[row - 1, col] != 9 && !boolHeightMapVisited[row - 1, col])
    {
        basinSize = GetBasinSize((row - 1, col), basinSize + 1, heightmap, boolHeightMapVisited);
    }

    if (point.col > 0 && heightmap[row, col - 1] != 9 && !boolHeightMapVisited[row, col - 1])
    {
        basinSize = GetBasinSize((row, col - 1), basinSize + 1, heightmap, boolHeightMapVisited);
    }

    if (point.row < heightmap.GetLength(0) - 1 && heightmap[row + 1, col] != 9 && !boolHeightMapVisited[row + 1, col])
    {
        basinSize = GetBasinSize((row + 1, col), basinSize + 1, heightmap, boolHeightMapVisited);
    }

    if (point.col < heightmap.GetLength(1) - 1 && heightmap[row, col + 1] != 9 && !boolHeightMapVisited[row, col + 1])
    {
        basinSize = GetBasinSize((row, col + 1), basinSize + 1, heightmap, boolHeightMapVisited);
    }

    return basinSize;
}