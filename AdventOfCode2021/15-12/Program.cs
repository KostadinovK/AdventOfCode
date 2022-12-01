using _15_12;

var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var map = new Dictionary<Point, int>();

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];

    for (var j = 0; j < line.Length; j++)
    {
        var point = new Point(j, i);

        if (!map.ContainsKey(point))
        {
            map[point] = 0;
        }

        map[point] = line[j] - '0';
    }
}

var part1Result = GetLowestRiskFromStartToEnd(map);

var scaledMap = ScaleUp(map);

var part2Result = GetLowestRiskFromStartToEnd(scaledMap);

Console.WriteLine(part1Result);
Console.WriteLine(part2Result);

//Dijkstra
int GetLowestRiskFromStartToEnd(Dictionary<Point, int> map)
{
    var start = new Point(0, 0);
    var end = new Point(map.Keys.MaxBy(p => p.x).x, map.Keys.MaxBy(p => p.y).y);

    var queue = new PriorityQueue<Point, int>();
    var totalRiskMap = new Dictionary<Point, int>();

    totalRiskMap[start] = 0;
    queue.Enqueue(start, 0);

    while (true)
    {
        var p = queue.Dequeue();

        if (p == end)
        {
            break;
        }

        foreach (var n in GetNeighbours(p))
        {
            if (map.ContainsKey(n))
            {
                var totalRiskThroughP = totalRiskMap[p] + map[n];
                if (totalRiskThroughP < totalRiskMap.GetValueOrDefault(n, int.MaxValue))
                {
                    totalRiskMap[n] = totalRiskThroughP;
                    queue.Enqueue(n, totalRiskThroughP);
                }
            }
        }
    }

    return totalRiskMap[end];
}

IEnumerable<Point> GetNeighbours(Point point) =>
    new[] {
        point with {y = point.y + 1},
        point with {y = point.y - 1},
        point with {x = point.x + 1},
        point with {x = point.x - 1},
    };

Dictionary<Point, int> ScaleUp(Dictionary<Point, int> map)
{
    var (ccol, crow) = (map.Keys.MaxBy(p => p.x).x + 1, map.Keys.MaxBy(p => p.y).y + 1);

    var res = new Dictionary<Point, int>(
        from y in Enumerable.Range(0, crow * 5)
        from x in Enumerable.Range(0, ccol * 5)

        // x, y and risk level in the original map:
        let tileY = y % crow
        let tileX = x % ccol
        let tileRiskLevel = map[new Point(tileX, tileY)]

        // risk level is increased by tile distance from origin:
        let tileDistance = (y / crow) + (x / ccol)

        // risk level wraps around from 9 to 1:
        let riskLevel = (tileRiskLevel + tileDistance - 1) % 9 + 1
        select new KeyValuePair<Point, int>(new Point(x, y), riskLevel)
    );

    return res;
}