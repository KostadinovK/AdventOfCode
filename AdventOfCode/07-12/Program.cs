var crabs = Console.ReadLine().Split(',').Select(int.Parse).ToList();


var leastFuel = int.MaxValue;

for (int i = 0; i < 2000; i++)
{
    var totalFuel = 0;

    for (int j = 0; j < crabs.Count; j++)
    {
        var distance = Math.Abs(crabs[j] - i);

        totalFuel += distance;
    }

    if (totalFuel < leastFuel)
    {
        leastFuel = totalFuel;
    }
}

Console.WriteLine(leastFuel);

leastFuel = int.MaxValue;

for (int i = 0; i < 2000; i++)
{
    var totalFuel = 0;

    for (int j = 0; j < crabs.Count; j++)
    {
        var distance = Math.Abs(crabs[j] - i);
        var fuelForDistance = 0;

        for (int k = 1; k <= distance; k++)
        {
            fuelForDistance += k;
        }

        totalFuel += fuelForDistance;
    }

    if (totalFuel < leastFuel)
    {
        leastFuel = totalFuel;
    }
}

Console.WriteLine(leastFuel);
