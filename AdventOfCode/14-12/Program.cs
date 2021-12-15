var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var countsByPair = new Dictionary<string, long>();
var pairInsertionRules = new Dictionary<string, char>();

var polymer = lines[0];

for (int i = 0; i < polymer.Length - 1; i++)
{
    var pair = "" + polymer[i] + polymer[i + 1];

    if (!countsByPair.ContainsKey(pair))
    {
        countsByPair.Add(pair, 0);
    }

    countsByPair[pair]++;
}

for (int i = 2; i < lines.Length; i++)
{
    var instruction = lines[i];
    var pair = instruction.Split(" -> ")[0];
    var newChar = instruction.Split(" -> ")[1].ToCharArray()[0];

    if (!pairInsertionRules.ContainsKey(pair))
    {
        pairInsertionRules.Add(pair, ' ');
    }

    pairInsertionRules[pair] = newChar;
}

for (int step = 0; step < 40; step++)
{
    var newCountsByPair = new Dictionary<string, long>();

    foreach (var kvp in countsByPair)
    {
        var (pair, count) = kvp;
        var (a, n, b) = (pair[0], pairInsertionRules[pair], pair[1]);

        newCountsByPair[$"{a}{n}"] = newCountsByPair.GetValueOrDefault($"{a}{n}") + count;
        newCountsByPair[$"{n}{b}"] = newCountsByPair.GetValueOrDefault($"{n}{b}") + count;
    }

    countsByPair = newCountsByPair;
}

var countsByLetter = new Dictionary<char, long>();

foreach (var (molecule, count) in countsByPair)
{
    var a = molecule[0];
    countsByLetter[a] = countsByLetter.GetValueOrDefault(a) + count;
}

countsByLetter[polymer.Last()]++;

Console.WriteLine(countsByLetter.Values.Max() - countsByLetter.Values.Min());
