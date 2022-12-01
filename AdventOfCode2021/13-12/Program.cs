var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var xCords = new List<int>();
var yCords = new List<int>();
var dots = new List<(int x, int y)>();
var paper = new char[1,1];
var foldInstructionsStartIndex = 0;


PopulatePaperWithDots();

Part1();

Part2();

PrintPaper();

void PopulatePaperWithDots()
{
    for (var i = 0; i < lines.Length; i++)
    {
        if (string.IsNullOrEmpty(lines[i]))
        {
            foldInstructionsStartIndex = i + 1;
            break;
        }

        var cord = lines[i].Split(',').Select(int.Parse).ToList();

        var x = cord[0];
        var y = cord[1];

        xCords.Add(x);
        yCords.Add(y);
        dots.Add((x, y));
    }

    var maxX = xCords.Max() + 1;
    var maxY = yCords.Max() + 1;

    paper = new char[maxY, maxX];

    for (int y = 0; y < paper.GetLength(0); y++)
    {
        for (int x = 0; x < paper.GetLength(1); x++)
        {
            if (dots.Contains((x, y)))
            {
                paper[y, x] = '#';
                continue;
            }

            paper[y, x] = '.';
        }
    }
}

void PrintPaper()
{
    for (int row = 0; row < paper.GetLength(0); row++)
    {
        for (int col = 0; col < paper.GetLength(1); col++)
        {
            Console.Write(paper[row, col] + " ");
        }

        Console.WriteLine();
    }
}

void Part1()
{
    var foldAxis = lines[foldInstructionsStartIndex].Split()[2].Split('=')[0];
    var foldNum = int.Parse(lines[foldInstructionsStartIndex].Split()[2].Split('=')[1]);

    var dotsCount = dots.Count;

    if (foldAxis == "y")
    {
        var mid = paper.GetLength(0) / 2;

        if (foldNum <= mid)
        {
            for (int y = 0; y < foldNum; y++)
            {
                for (int x = 0; x < paper.GetLength(1); x++)
                {
                    if (paper[y, x] == '#')
                    {
                        var mirrorY = foldNum + (foldNum - y);

                        if (paper[mirrorY, x] == '#')
                        {
                            dotsCount--;
                        }

                        paper[mirrorY, x] = '#';
                    }
                }
            }
        }
        else
        {
            for (int y = foldNum + 1; y < paper.GetLength(0); y++)
            {
                for (int x = 0; x < paper.GetLength(1); x++)
                {
                    if (paper[y, x] == '#')
                    {
                        var mirrorY = foldNum + (foldNum - y);

                        if (paper[mirrorY, x] == '#')
                        {
                            dotsCount--;
                        }

                        paper[mirrorY, x] = '#';
                    }
                }
            }
        }
    }
    else
    {
        var mid = paper.GetLength(1) / 2;

        if (foldNum <= mid)
        {
            for (int y = 0; y < paper.GetLength(0); y++)
            {
                for (int x = 0; x < foldNum; x++)
                {
                    if (paper[y, x] == '#')
                    {
                        var mirrorX = foldNum + (foldNum - x);

                        if (paper[y, mirrorX] == '#')
                        {
                            dotsCount--;
                        }

                        paper[y, mirrorX] = '#';
                    }
                }
            }
        }
        else
        {
            for (int y = 0; y < paper.GetLength(0); y++)
            {
                for (int x = foldNum + 1; x < paper.GetLength(1); x++)
                {
                    if (paper[y, x] == '#')
                    {
                        var mirrorX = foldNum + (foldNum - x);

                        if (paper[y, mirrorX] == '#')
                        {
                            dotsCount--;
                        }

                        paper[y, mirrorX] = '#';
                    }
                }
            }
        }
    }

    Console.WriteLine(dotsCount);
}

void Part2()
{
    for (int i = foldInstructionsStartIndex; i < lines.Length; i++)
    {
        var foldAxis = lines[i].Split()[2].Split('=')[0];
        var foldNum = int.Parse(lines[i].Split()[2].Split('=')[1]);

        if (foldAxis == "y")
        {
            var mid = paper.GetLength(0) / 2;
            var foldedPaper = new char[paper.GetLength(0) - (foldNum + 1), paper.GetLength(1)];

            if (foldNum <= mid)
            {
                for (int y = 0; y < foldNum; y++)
                {
                    for (int x = 0; x < paper.GetLength(1); x++)
                    {
                        if (paper[y, x] == '#')
                        {
                            var mirrorY = foldNum + (foldNum - y);

                            paper[mirrorY, x] = '#';
                        }
                    }
                }

                for (int row = 0; row < foldedPaper.GetLength(0); row++)
                {
                    for (int col = 0; col < foldedPaper.GetLength(1); col++)
                    {
                        foldedPaper[row, col] = paper[mid + 1 + row, col];
                    }
                }

                paper = foldedPaper;
            }
            else
            {
                for (int y = foldNum + 1; y < paper.GetLength(0); y++)
                {
                    for (int x = 0; x < paper.GetLength(1); x++)
                    {
                        if (paper[y, x] == '#')
                        {
                            var mirrorY = foldNum + (foldNum - y);

                            paper[mirrorY, x] = '#';
                        }
                    }
                }

                for (int row = 0; row < foldedPaper.GetLength(0); row++)
                {
                    for (int col = 0; col < foldedPaper.GetLength(1); col++)
                    {
                        foldedPaper[row, col] = paper[row, col];
                    }
                }

                paper = foldedPaper;
            }
        }
        else
        {
            var mid = paper.GetLength(1) / 2;
            var foldedPaper = new char[paper.GetLength(0), paper.GetLength(1) - (foldNum + 1)];

            if (foldNum <= mid)
            {
                for (int y = 0; y < paper.GetLength(0); y++)
                {
                    for (int x = 0; x < foldNum; x++)
                    {
                        if (paper[y, x] == '#')
                        {
                            var mirrorX = foldNum + (foldNum - x);

                            paper[y, mirrorX] = '#';
                        }
                    }
                }

                for (int row = 0; row < foldedPaper.GetLength(0); row++)
                {
                    for (int col = 0; col < foldedPaper.GetLength(1); col++)
                    {
                        foldedPaper[row, col] = paper[row, mid + 1 + col];
                    }
                }

                paper = foldedPaper;
            }
            else
            {

                for (int y = 0; y < paper.GetLength(0); y++)
                {
                    for (int x = foldNum + 1; x < paper.GetLength(1); x++)
                    {
                        if (paper[y, x] == '#')
                        {
                            var mirrorX = foldNum + (foldNum - x);

                            paper[y, mirrorX] = '#';
                        }
                    }
                }

                for (int row = 0; row < foldedPaper.GetLength(0); row++)
                {
                    for (int col = 0; col < foldedPaper.GetLength(1); col++)
                    {
                        foldedPaper[row, col] = paper[row, col];
                    }
                }

                paper = foldedPaper;
            }
        }
    }

}
