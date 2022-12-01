var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var grid = new int[10, 10];
var hasFlashedThisStep = new bool[10, 10];



for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        var octopus = lines[i][j] - '0';

        grid[i, j] = octopus;
    }
}

var flashes = 0;
var areSynced = false;
var step = 0;
var totalflashes = 0;

while (!areSynced)
{
    if (step == 100)
    {
        totalflashes = flashes;
    }

    hasFlashedThisStep = new bool[10, 10];

    for (int i = 0; i < grid.GetLength(0); i++)
    {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            IncrementOctopusEnergy(i, j);
        }
    }

    areSynced = true;

    for (int i = 0; i < hasFlashedThisStep.GetLength(0); i++)
    {
        for (int j = 0; j < hasFlashedThisStep.GetLength(1); j++)
        {
            if (!hasFlashedThisStep[i, j])
            {
                areSynced = false;
            }
        }
    }

    step++;
}

Console.WriteLine("Flashes after 100 steps: " + totalflashes);
Console.WriteLine("Step = " + step);

void IncrementOctopusEnergy(int row, int col)
{
    if (row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1))
    {
        return;
    }

    if (hasFlashedThisStep[row, col])
    {
        return;
    }

    grid[row, col]++;

    if (grid[row, col] > 9)
    {
        hasFlashedThisStep[row, col] = true;
        grid[row, col] = 0;
        flashes++;

        // up, down, left and right
        IncrementOctopusEnergy(row - 1, col);
        IncrementOctopusEnergy(row + 1, col);
        IncrementOctopusEnergy(row, col - 1);
        IncrementOctopusEnergy(row, col + 1);
        //diagonals
        IncrementOctopusEnergy(row - 1, col - 1);
        IncrementOctopusEnergy(row - 1, col + 1);
        IncrementOctopusEnergy(row + 1, col - 1);
        IncrementOctopusEnergy(row + 1, col + 1);
    }
}