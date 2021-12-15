var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var cavern = new int[lines.Length, lines[0].Length];
var visited = new bool[lines.Length, lines[0].Length];

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];

    for (var j = 0; j < line.Length; j++)
    {
        var risk = line[j] - '0';

        cavern[i, j] = risk;
    }
}

var pathsRisk = new List<int>();

GetAllPathsRisk(0, 0, 0);

var pathWithLowestRisk = pathsRisk.Min();

Console.WriteLine(pathWithLowestRisk);

int GetAllPathsRisk(int row, int col, int risk)
{
    if (row < 0 || row >= cavern.GetLength(0) || col < 0 || col >= cavern.GetLength(1))
    {
        return risk;
    }

    if (visited[row, col])
    {
        return risk;
    }

    var value = cavern[row, col];

    if (row == cavern.GetLength(0) - 1 && col == cavern.GetLength(1) - 1)
    {
        pathsRisk.Add(risk + value);
        return risk;
    }

    if (row != 0 || col != 0)
    {
        risk += value;
    }

    visited[row, col] = true;

    risk = GetAllPathsRisk(row - 1, col, risk);
    risk = GetAllPathsRisk(row + 1, col, risk);
    risk = GetAllPathsRisk(row, col - 1, risk);
    risk = GetAllPathsRisk(row, col + 1, risk);

    visited[row, col] = false;

    if (row != 0 || col != 0)
    {
        risk -= value;
    }

    return risk;
}