var depth = 0;
var horizontalPosition = 0;
var aim = 0;

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
            depth += aim * units;
            break;
        case "up":
            aim -= units;
            break;
        case "down":
            aim += units;
            break;
    }
}

Console.WriteLine(horizontalPosition * depth);
