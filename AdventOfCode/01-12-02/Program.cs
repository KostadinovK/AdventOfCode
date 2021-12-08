var depths = new List<int>();

while (true)
{
    var depthString = Console.ReadLine();

    if (string.IsNullOrEmpty(depthString))
    {
        break;
    }

    var depth = int.Parse(depthString);

    depths.Add(depth);
}

var sums = new List<int>();

for (var i = 0; i <= depths.Count - 3; i++)
{
    var slidingWindowSum = 0;

    for (var j = i; j <= i + 2; j++)
    {
        slidingWindowSum += depths[j];
    }

    sums.Add(slidingWindowSum);
}

var counter = 0;

for (int i = 0; i < sums.Count - 1; i++)
{
    if (sums[i] < sums[i + 1])
    {
        counter++;
    }
}

Console.WriteLine(counter);