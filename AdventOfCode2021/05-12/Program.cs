var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var field = new int[1000, 1000];

foreach (var line in lines)
{
    var cordsPair = line.Split(" -> ").ToList();
    var firstCord = cordsPair[0].Split(',').Select(int.Parse).ToList();
    var secondCord = cordsPair[1].Split(',').Select(int.Parse).ToList();

    var x1 = firstCord[0];
    var y1 = firstCord[1];

    var x2 = secondCord[0];
    var y2 = secondCord[1];

    if (x1 != x2 && y1 != y2)
    {
        continue;
    }


    if (y1 == y2)
    {
        var bigger = Math.Max(x1, x2);
        var smaller = Math.Min(x1, x2);

        for (var i = smaller; i <= bigger; i++)
        {
            field[y1, i]++;
        }
    }


    if (x1 == x2)
    {
        var bigger = Math.Max(y1, y2);
        var smaller = Math.Min(y1, y2);

        for (var i = smaller; i <= bigger; i++)
        {
            field[i, x1]++;
        }
    }
}

var points = 0;

for (int row = 0; row < field.GetLength(0); row++)
{
    for (int col = 0; col < field.GetLength(1); col++)
    {
        if (field[row, col] > 1)
        {
            points++;
        }
    }
}

Console.WriteLine(points);

field = new int[1000, 1000];

foreach (var line in lines)
{
    var cordsPair = line.Split(" -> ").ToList();
    var firstCord = cordsPair[0].Split(',').Select(int.Parse).ToList();
    var secondCord = cordsPair[1].Split(',').Select(int.Parse).ToList();

    var x1 = firstCord[0];
    var y1 = firstCord[1];

    var x2 = secondCord[0];
    var y2 = secondCord[1];

    if (y1 == y2)
    {
        var bigger = Math.Max(x1, x2);
        var smaller = Math.Min(x1, x2);

        for (var i = smaller; i <= bigger; i++)
        {
            field[y1, i]++;
        }
    }


    if (x1 == x2)
    {
        var bigger = Math.Max(y1, y2);
        var smaller = Math.Min(y1, y2);

        for (var i = smaller; i <= bigger; i++)
        {
            field[i, x1]++;
        }
    }

    if (x1 != x2 && y1 != y2)
    {
        var biggerX = Math.Max(x1, x2);
        var smallerX = Math.Min(x1, x2);

        var biggerY = Math.Max(y1, y2);
        var smallerY = Math.Min(y1, y2);

        var isCord1Bigger = biggerX == x1 && biggerY == y1;
        var isCord2Bigger = biggerX == x2 && biggerY == y2;

        for (var i = 0; i <= biggerY - smallerY; i++)
        {
            for (var j = 0; j <= biggerX - smallerX; j++)
            {
                var row = i + smallerY;
                var col = j + smallerX;

                if (isCord1Bigger || isCord2Bigger)
                {
                    if (i == j)
                    {
                        field[row, col]++;
                    }
                }
                else if (!isCord1Bigger && !isCord2Bigger)
                {
                    if (col == biggerX - i)
                    {
                        field[row, col]++;
                    }
                }
               
            }
        }
    }
}

points = 0;

for (int row = 0; row < field.GetLength(0); row++)
{
    for (int col = 0; col < field.GetLength(1); col++)
    {
        if (field[row, col] > 1)
        {
            points++;
        }
    }
}

Console.WriteLine(points);
