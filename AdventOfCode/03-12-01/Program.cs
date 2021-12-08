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

var gammaRateBin = new StringBuilder();
var epsilonRateBin = new StringBuilder();

for (var i = 0; i < binaryNumbers[0].Length; i++)
{
    var nullsCount = 0;
    var onesCount = 0;

    foreach (var binaryNumber in binaryNumbers)
    {
        var bit = binaryNumber[i];

        if (bit == '1')
        {
            onesCount++;
        } 
        else if (bit == '0')
        {
            nullsCount++;
        }
    }

    if (onesCount > nullsCount)
    {
        gammaRateBin.Append('1');
        epsilonRateBin.Append('0');
    }
    else
    {
        gammaRateBin.Append('0');
        epsilonRateBin.Append('1');
    }
}

var gammaRate = Convert.ToInt32(gammaRateBin.ToString(), 2);
var epsilonRate = Convert.ToInt32(epsilonRateBin.ToString(), 2);

Console.WriteLine(gammaRate * epsilonRate);

