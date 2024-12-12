using System.Text.RegularExpressions;

var path = Directory.GetCurrentDirectory();
var input = File.ReadAllText(@$"{path}\input.txt");

long sum = 0;

var operationsPattern = @"do\(\)|don't\(\)|mul\((\d{1,3}),(\d{1,3})\)";
var matches = Regex.Matches(input, operationsPattern);

var isMulCommandEnabled = true;
foreach (Match match in matches)
{
    switch (match.Value)
    {
        case "do()":
            isMulCommandEnabled = true;
            break;
        case "don't()":
            isMulCommandEnabled = false;
            break;
        default:
            if (!isMulCommandEnabled)
            {
                break;
            }
            var firstNumber = int.Parse(match.Groups[1].Value);
            var secondNumber = int.Parse(match.Groups[2].Value);

            sum += firstNumber * secondNumber;
            break;
    }
}

Console.WriteLine($"Sum: {sum}");