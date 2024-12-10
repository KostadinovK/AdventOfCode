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
        var reportIsSafe = true;
        var levelRemovedCount = 0;
        var prevLevelsDiff = Int32.MaxValue;
        for (int i = 0; i < report.Count - 1; i++)
        {
            var currLevel = report[i];
            var nextLevel = report[i + 1];

            var diff = currLevel - nextLevel;

            if (diff == 0 || diff < -3 || diff > 3)
            {
                levelRemovedCount++;
                if (levelRemovedCount > 1)
                {
                    reportIsSafe = false;
                    break;
                }
                report[i + 1] = currLevel;
                continue;
            }

            if (prevLevelsDiff != Int32.MaxValue && Math.Sign(prevLevelsDiff) != Math.Sign(diff))
            {
                levelRemovedCount++;
                if (levelRemovedCount > 1)
                {
                    reportIsSafe = false;
                    break;
                }
                report[i + 1] = currLevel;
                continue;
            }

            prevLevelsDiff = diff;
        }

        if (reportIsSafe)
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
        var reportIsSafe = true;
        var prevLevelsDiff = Int32.MaxValue;
        for (int i = 0; i < report.Count - 1; i++)
        {
            var currLevel = report[i];
            var nextLevel = report[i + 1];

            var diff = currLevel - nextLevel;

            if (diff == 0 || diff < -3 || diff > 3)
            {
                reportIsSafe = false;
                break;
            }

            if (prevLevelsDiff != Int32.MaxValue && Math.Sign(prevLevelsDiff) != Math.Sign(diff))
            {
                reportIsSafe = false;
                break;
            }

            prevLevelsDiff = diff;
        }

        if (reportIsSafe)
        {
            safeReports++;
        }
    }

    Console.WriteLine($"Safe reports count: {safeReports}");
}
