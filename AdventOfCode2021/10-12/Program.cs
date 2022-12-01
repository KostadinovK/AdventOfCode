var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var openBrackets = new [] { '(', '[', '{', '<' };
var closeBrackets = new[] { ')', ']', '}', '>' };

var bracketPairs = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };

var invalidBracketsToPoints = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };

var closedBracketMissedPoints = new Dictionary<char, int> { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };

var sum = 0;
var linePoints = new List<long>();

foreach (var line in lines)
{
    var brackets = new Stack<char>();
    var isCorrupted = false;

    for (int i = 0; i < line.Length; i++)
    {
        var bracket = line[i];

        if (openBrackets.Contains(bracket))
        {
            brackets.Push(bracket);
            continue;
        }

        if (closeBrackets.Contains(bracket))
        {
            var removedBracket = brackets.Pop();

            if (bracketPairs[bracket] != removedBracket)
            {
                isCorrupted = true;
                sum += invalidBracketsToPoints[bracket];
            }
        }
    }

    if (!isCorrupted && brackets.Count != 0)
    {
        long points = 0;

        while (brackets.Count != 0)
        {
            var removedBracket = brackets.Pop();

            points *= 5;
            points += closedBracketMissedPoints[removedBracket];
        }

        linePoints.Add(points);
    }
}

Console.WriteLine(sum);

linePoints = linePoints.OrderBy(x => x).ToList();

var mid = linePoints[linePoints.Count / 2];

Console.WriteLine(mid);
