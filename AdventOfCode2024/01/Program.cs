var path = Directory.GetCurrentDirectory();
var lines = File.ReadAllLines(@$"{path}\input.txt");

var leftNumbers = new List<int>();
var rightNumbers = new List<int>();

foreach (var line in lines)
{
    var numbers = line.Split(' ');
    numbers = numbers.Where(x => !string.IsNullOrEmpty(x)).ToArray();

    var firstNumber = int.Parse(numbers[0]);
    var secondNumber = int.Parse(numbers[1]);

    leftNumbers.Add(firstNumber);
    rightNumbers.Add(secondNumber);
}

if (leftNumbers.Count != rightNumbers.Count)
{
    throw new ArgumentOutOfRangeException("The two lists must have the same count");
}



SolveTotalDistanceSum(leftNumbers, rightNumbers);
SolveSimilarityScore(leftNumbers, rightNumbers);
return;

void SolveTotalDistanceSum(List<int> leftList, List<int> rightList)
{
    leftList.Sort();
    rightList.Sort();

    long distanceSum = 0;
    for (int i = 0; i < leftList.Count; i++)
    {
        var distanceBetweenNumbers = Math.Abs(leftList[i] - rightList[i]);
        distanceSum += distanceBetweenNumbers;
    }

    Console.WriteLine($"Total Distance Sum: {distanceSum}");
}

void SolveSimilarityScore(List<int> leftList, List<int> rightList)
{
    var leftNumbersOcurencesInRightList = new Dictionary<int, int>();

    foreach (var leftNum in leftList)
    {
        if (leftNumbersOcurencesInRightList.ContainsKey(leftNum))
        {
            continue;
        }

        var occurrences = 0;
        foreach (var rightNum in rightList)
        {
            if (leftNum == rightNum)
            {
                occurrences++;
            }
        }

        leftNumbersOcurencesInRightList.Add(leftNum, occurrences);
    }

    long similarityScore = 0;

    foreach (var (number, numberOfOccurrences) in leftNumbersOcurencesInRightList)
    {
        similarityScore += number * numberOfOccurrences;
    }

    Console.WriteLine($"Total Similarity Score: {similarityScore}");
}
