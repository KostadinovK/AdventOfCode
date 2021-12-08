var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var segmentsCountForDesiredDigits = new [] { 2, 3, 4, 7};

var desiredDigitsCount = 0;
foreach (var line in lines)
{
   
    var outputDigitsString = line.Split(" | ")[1];
    var outputDigits = outputDigitsString.Split(' ').ToList();

    foreach (var digit in outputDigits)
    {
        if (segmentsCountForDesiredDigits.Contains(digit.Length))
        {
            desiredDigitsCount++;
        }
    }
}

Console.WriteLine(desiredDigitsCount);

var totalSum = 0;

foreach (var line in lines)
{
    var inputDigitsString = line.Split(" | ")[0];
    var outputDigitString = line.Split(" | ")[1];

    var input = inputDigitsString.Split(' ').ToList();
    var output = outputDigitString.Split(' ').ToList();

    var one = input.Single(x => x.Length == 2);
    var four = input.Single(x => x.Length == 4);
    var seven = input.Single(x => x.Length == 3);
    var eight = input.Single(x => x.Length == 7);

    var nine = input.Single(x => x.Length == 6 && x.Except(seven).Except(four).Count() == 1);

    var six = input.Single(x => x.Length == 6 && x != nine && one.Except(x).Count() == 1);

    var zero = input.Single(x => x.Length == 6 && x != nine && x != six);

    var e = eight.Except(nine).Single();
    var c = eight.Except(six).Single();
    var f = one.Except(new[] { c }).Single();

    var five = input.Single(x => x.Length == 5 && !x.Contains(c) && !x.Contains(e));
    
    var two = input.Single(x => x.Length == 5 && x != five && x.Contains(c) && !x.Contains(f));
   
    var three = input.Single(x => x.Length == 5 && x != five && x != two);

    var numbers = new [] {
        zero,
        one,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine
    };

    var outputNumber = 0;
    var multiplier = 1000;

    for (int i = 0; i < output.Count; i++)
    {
        var digit = output[i];

        for (int j = 0; j < numbers.Length; j++)
        {
            var segment = numbers[j];

            if (digit.Length == segment.Length && !digit.Except(segment).Any() && !segment.Except(digit).Any())
            {
                outputNumber += j * multiplier;
                multiplier /= 10;
            }
        }
    }

    totalSum += outputNumber;
}

Console.WriteLine(totalSum);