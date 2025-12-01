using System;

const int MaxDialValue = 99;
const int MinDialValue = 0;

var path = Directory.GetCurrentDirectory();
var rotationInstructions = File.ReadAllLines(@$"{path}\input.txt");

SolvePart1(rotationInstructions, MinDialValue, MaxDialValue);
SolvePart2(rotationInstructions, MinDialValue, MaxDialValue);


void SolvePart1(string[] instructions, int minDialValue, int maxDialValue)
{
    var dialValue = 50;
    var password = 0;
    foreach (var instruction in instructions)
    {
        var direction = instruction[0];
        var stepsStr = instruction.Substring(1);
        var steps = int.Parse(stepsStr);
        if (direction == 'L')
        {
            if (dialValue - steps < minDialValue)
            {
                dialValue = (dialValue - steps + 10_000) % 100;
            }
            else
            {
                dialValue -= steps;
            }
        }
        else if (direction == 'R')
        {
            if (dialValue + steps > maxDialValue)
            {
                dialValue = (dialValue + steps) % 100;
            }
            else
            {
                dialValue += steps;
            }
        }

        if (dialValue == 0)
        {
            password++;
        }
    }

    Console.WriteLine($"Part 1 - The password is: {password}");
}

void SolvePart2(string[] instructions, int minDialValue, int maxDialValue)
{
    var dialValue = 50;
    var password = 0;
    foreach (var instruction in instructions)
    {
        var direction = instruction[0];
        var stepsStr = instruction.Substring(1);
        var steps = int.Parse(stepsStr);
        if (direction == 'L')
        {
            password += ((steps - dialValue) / 100) + (dialValue > 0 && steps >= dialValue ? 1 : 0);
            if (dialValue - steps < minDialValue)
            {
                dialValue = (dialValue - steps + 10_000) % 100;
            }
            else
            {
                dialValue -= steps;
            }
        } else if (direction == 'R')
        {
            password += (dialValue + steps) / 100;
            if (dialValue + steps > maxDialValue)
            {
                dialValue = (dialValue + steps) % 100;
            }
            else
            {
                dialValue += steps;
            }
        }
    }

    Console.WriteLine($"Part 2 - The password is: {password}");
}