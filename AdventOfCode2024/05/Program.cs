using System.Collections.Generic;

var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var emptyLineIndex = lines.Select((value, index) => (value, index))
    .FirstOrDefault(valueIndex => string.IsNullOrEmpty(valueIndex.value)).index;

var instructionsByNumberDict = new Dictionary<int, List<int>>();
for (int i = 0; i < emptyLineIndex; i++)
{
    var pageNumbers = lines[i].Split('|').Select(int.Parse).ToList();

    if (!instructionsByNumberDict.ContainsKey(pageNumbers[0]))
    {
        instructionsByNumberDict[pageNumbers[0]] = new List<int>();
    }

    instructionsByNumberDict[pageNumbers[0]].Add(pageNumbers[1]);
}

PrintCorrectlyOrderedPagesUpdatesMiddleNumberSum(emptyLineIndex, lines, instructionsByNumberDict);
PrintInCorrectlyOrderedPagesUpdatesMiddleNumberSum(emptyLineIndex, lines, instructionsByNumberDict);

void PrintInCorrectlyOrderedPagesUpdatesMiddleNumberSum(int emptyLineIndex1, string[] lines, Dictionary<int, List<int>> instructionByPageNumberDict)
{
    var pageNumberComparator = new PagesComparer(instructionsByNumberDict);
    long inValidLinesMiddleNumSum = 0;
    for (int i = emptyLineIndex1 + 1; i < lines.Length; i++)
    {
        var pageNumbersLine = lines[i].Split(',').Select(int.Parse).ToList();

        var isValidLine = IsPageNumberLineValid(pageNumbersLine, instructionByPageNumberDict);

        if (!isValidLine)
        {
            Console.WriteLine($"Original: {string.Join(",", pageNumbersLine)}");
            pageNumbersLine.Sort(pageNumberComparator);
            Console.WriteLine($"Sorted: {string.Join(",", pageNumbersLine)}");
            var middleIndex = pageNumbersLine.Count / 2;
            inValidLinesMiddleNumSum += pageNumbersLine[middleIndex];
        }
    }

    Console.WriteLine($"Invalid lines sum: {inValidLinesMiddleNumSum}");
}

void PrintCorrectlyOrderedPagesUpdatesMiddleNumberSum(int emptyLineIndex1, string[] lines, Dictionary<int, List<int>> instructionByPageNumberDict)
{
    long validLinesMiddleNumSum = 0;
    for (int i = emptyLineIndex1 + 1; i < lines.Length; i++)
    {
        var pageNumbersLine = lines[i].Split(',').Select(int.Parse).ToList();

        var isValidLine = IsPageNumberLineValid(pageNumbersLine, instructionByPageNumberDict);

        if (isValidLine)
        {
            var middleIndex = pageNumbersLine.Count / 2;
            validLinesMiddleNumSum += pageNumbersLine[middleIndex];
        }
    }

    Console.WriteLine($"Valid lines sum: {validLinesMiddleNumSum}");
}

bool IsPageNumberLineValid(List<int> pageNumbers, Dictionary<int, List<int>> pageRulesByPageNumber)
{
    var isValid = true;

    for (int j = 0; j < pageNumbers.Count; j++)
    {
        var pageNumber = pageNumbers[j];
        var pagesBeforeRules = pageRulesByPageNumber.Where(x => x.Value.Contains(pageNumber)).Select(x => x.Key).ToList();

        var matchTheBeforeRules = pageNumbers.Where((value, index) => index < j).ToList().All(x => pagesBeforeRules.Contains(x));

        if (matchTheBeforeRules && !pageRulesByPageNumber.ContainsKey(pageNumber))
        {
            continue;
        }

        var pagesAfterRules = pageRulesByPageNumber[pageNumber];

        var matchTheAfterRules = pageNumbers.Where((value, index) => index > j).ToList().All(x => pagesAfterRules.Contains(x));

        if (!matchTheBeforeRules || !matchTheAfterRules)
        {
            isValid = false;
            break;
        }
    }

    return isValid;
}

public class PagesComparer : IComparer<int>
{
    private readonly Dictionary<int, List<int>> _instructionsByNumberDict;

    public PagesComparer(Dictionary<int, List<int>> instructionsByNumberDict)
    {
        _instructionsByNumberDict = instructionsByNumberDict;
    }

    public int Compare(int x, int y)
    {
        var xPagesAfter = _instructionsByNumberDict.ContainsKey(x) ? _instructionsByNumberDict[x] : new List<int>();
        var xPagesBefore = _instructionsByNumberDict.Where(kvp => kvp.Value.Contains(x)).Select(kvp => kvp.Key).ToList();

        if (xPagesAfter.Contains(y))
        {
            return -1;
        }

        if (xPagesBefore.Contains(y))
        {
            return 1;
        }

        return 0;
    }
}

