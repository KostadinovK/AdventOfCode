var depth = 0;
var horizontalPosition = 0;

while (true)
{
    var command = Console.ReadLine();

    if (string.IsNullOrEmpty(command))
    {
        break;
    }

    var direction = command.Split(" ")[0];
    var units = int.Parse(command.Split(" ")[1]);

    switch (direction)
    {
        case "forward":
            horizontalPosition += units;
            break;
        case "up":
            depth -= units;
            break;
        case "down":
            depth += units;
            break;
    }
}

Console.WriteLine(horizontalPosition * depth);
