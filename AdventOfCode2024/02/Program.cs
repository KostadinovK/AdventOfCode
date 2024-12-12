var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var reports = new List<List<int>>();
foreach (var line in lines)
{
    var report = new List<int>();
    var numbers = line.Split(' ');

    foreach (var number in numbers)
    {
        report.Add(int.Parse(number));
    }

    reports.Add(report);
}

DisplaySafeRepostsCount(reports);
DisplaySafeRepostsCountWithProblemDampener(reports);

void DisplaySafeRepostsCountWithProblemDampener(List<List<int>> reports)
{
    var safeReports = 0;

    foreach (var report in reports)
    {
        var reportVariationsWithOneRemoval = report
            .Select((_, index) => report
                .Where((_, index2) => index != index2).ToList())
            .ToList();

        var hasSafeReportVariation = reportVariationsWithOneRemoval.Any(IsReportSafe);

        if (hasSafeReportVariation)
        {
            safeReports++;
        }
    }

    Console.WriteLine($"Safe reports count with Problem Damper: {safeReports}");
}


void DisplaySafeRepostsCount(List<List<int>> reports)
{
    var safeReports = 0;

    foreach (var report in reports)
    {
        var reportIsSafe = IsReportSafe(report);

        if (reportIsSafe)
        {
            safeReports++;
        }
    }

    Console.WriteLine($"Safe reports count: {safeReports}");
}

bool IsReportSafe(List<int> report)
{
    var isSafe = true;
    var prevLevelsDiff = Int32.MaxValue;
    for (int i = 0; i < report.Count - 1; i++)
    {
        var currLevel = report[i];
        var nextLevel = report[i + 1];

        var diff = currLevel - nextLevel;

        if (diff == 0 || diff < -3 || diff > 3)
        {
            isSafe = false;
            break;
        }

        if (prevLevelsDiff != Int32.MaxValue && Math.Sign(prevLevelsDiff) != Math.Sign(diff))
        {
            isSafe = false;
            break;
        }

        prevLevelsDiff = diff;
    }

    return isSafe;
}
