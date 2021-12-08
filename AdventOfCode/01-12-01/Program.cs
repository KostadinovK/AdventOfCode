var depth = -1;
var counter = 0;

while (true)
{
    var depthString = Console.ReadLine();

    if (string.IsNullOrEmpty(depthString))
    {
        break;
    }

    if (depth >= 0 && int.Parse(depthString) > depth)
    {
        counter++;
    }

    depth = int.Parse(depthString);
}

Console.WriteLine(counter);
