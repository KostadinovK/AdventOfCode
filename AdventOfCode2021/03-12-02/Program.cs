using System.Text;

var binaryNumbers = new List<string>();

while (true)
{
    var binaryNumber = Console.ReadLine();

    if (string.IsNullOrEmpty(binaryNumber))
    {
        break;
    }

    binaryNumbers.Add(binaryNumber);
}

var oxygenNumberBin = GetBinaryNumber(binaryNumbers, '0', '1');
var scrubberNumberBin = GetBinaryNumber(binaryNumbers, '1', '0');

var oxygen = Convert.ToInt32(oxygenNumberBin, 2);
var scrubber = Convert.ToInt32(scrubberNumberBin, 2);

Console.WriteLine(oxygen * scrubber);


string GetBinaryNumber(List<string> binaryNumbers, char charToPutIfNullsAreMore, char charToPutIfOnesAreMore)
{
    for (var i = 0; i < binaryNumbers[0].Length; i++)
    {
        var nullsCount = 0;
        var onesCount = 0;

        if (binaryNumbers.Count == 1)
        {
            break;
        }

        foreach (var num in binaryNumbers)
        {
            var bit = num[i];

            if (bit == '1')
            {
                onesCount++;
            }
            else if (bit == '0')
            {
                nullsCount++;
            }
        }

        if (onesCount >= nullsCount)
        {
            binaryNumbers = binaryNumbers.Where(b => b[i] == charToPutIfOnesAreMore).ToList();
        }
        else
        {
            binaryNumbers = binaryNumbers.Where(b => b[i] == charToPutIfNullsAreMore).ToList();
        }
    }

    return binaryNumbers[0];
}
