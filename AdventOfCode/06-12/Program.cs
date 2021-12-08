var fish = Console.ReadLine().Split(',').Select(long.Parse).ToList();

var countByAge = new long[9];

foreach (var f in fish)
{
    countByAge[f]++;
}

for (var day = 1; day <= 256; day++)
{
    var newFish = countByAge[0];

    for (int age = 0; age < countByAge.Length - 1; age++)
    {
        countByAge[age] = countByAge[age + 1];
    }

    countByAge[8] = newFish;
    countByAge[6] += newFish;
}

long total = 0;

for (int i = 0; i < countByAge.Length; i++)
{
    total += countByAge[i];
}

Console.WriteLine(total);