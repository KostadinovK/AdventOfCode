using System.Text;

var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var graph = new Dictionary<string, List<string>>();
var visited = new Dictionary<string, bool>();

foreach (var line in lines)
{
    var first = line.Split("-").ToList()[0];
    var second = line.Split("-").ToList()[1];

    if (!graph.ContainsKey(first))
    {
        graph[first] = new List<string>();
    }

    if (!graph.ContainsKey(second))
    {
        graph[second] = new List<string>();
    }

    if (!graph[first].Contains(second))
    {
        graph[first].Add(second);
    }

    if (!graph[second].Contains(first))
    {
        graph[second].Add(first);
    }

    if (first.ToLower() == first)
    {
        visited[first] = false;
    }

    if (second.ToLower() == second)
    {
        visited[second] = false;
    }
}

var way = new Stack<string>();
var paths = 0;

DFS("start");

Console.WriteLine(paths);

paths = 0;
var hasANodeBeenVisitedTwice = false;
var visitedTwiceNode = "";

DFSPart2("start");

Console.WriteLine(paths);

void DFS(string node)
{
    if (visited.ContainsKey(node) && !visited[node])
    {
        visited[node] = true;
    }

    way.Push(node);

    if (node == "end")
    {
        paths++;
        Console.WriteLine(string.Join(' ', way.Reverse()));
        visited[node] = false;
        way.Pop();
        return;
    }

    foreach (var n in graph[node])
    {
        if (visited.ContainsKey(n) && visited[n])
        { 
            continue;
        }

        DFS(n);
    }

    way.Pop();

    if (visited.ContainsKey(node))
    {
        visited[node] = false;
    }
}

void DFSPart2(string node)
{
    if (node == "start" && visited[node])
    {
        return;
    }

    if (!hasANodeBeenVisitedTwice && visited.ContainsKey(node) && visited[node])
    {
        hasANodeBeenVisitedTwice = true;
        visitedTwiceNode = node;
    }

    if (visited.ContainsKey(node) && !visited[node])
    {
        visited[node] = true;
    }

    way.Push(node);

    if (node == "end")
    {
        paths++;
        visited[node] = false;
        return;
    }

    foreach (var n in graph[node])
    {
        if (hasANodeBeenVisitedTwice && visited.ContainsKey(n) && visited[n])
        {
            continue;
        }

        DFSPart2(n);
    }

    if (visited.ContainsKey(node))
    {
        visited[node] = false;
    }

    if (hasANodeBeenVisitedTwice && visitedTwiceNode == node)
    {
        hasANodeBeenVisitedTwice = false;
        visited[node] = true;
    }
}