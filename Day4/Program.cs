Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n').ToList();

int sum1 = 0;
int sum2 = 0;

List<int> cardsCount = Enumerable.Range(0, lines.Count).Select(i => i = 1).ToList();

for (int i=0;i<lines.Count; i++)
{
    string line = lines[i];
    int cardNumber = int.Parse(line.Split(":")[0].Replace(" ", "").Split("d")[1]);
    string[] cardNumbers = line.Split(":")[1].Split("|");
    List<int> winningNumbers = cardNumbers[0].Split(" ").Where(e => !string.IsNullOrEmpty(e) && e.All(char.IsDigit)).Select(e => int.Parse(e)).ToList();
    List<int> numbers = cardNumbers[1].Split(" ").Where(e => !string.IsNullOrEmpty(e) && e.All(char.IsDigit)).Select(e => int.Parse(e)).ToList();

    int matchingNumbersCount = numbers.Where(winningNumbers.Contains).ToList().Count;
    sum1 += (int)Math.Pow(2, matchingNumbersCount - 1);

    for(int j = 0; j<matchingNumbersCount; j++)
    {
        cardsCount[i+j+1] += cardsCount[i];
    }
}
sum2 = cardsCount.Sum();

Console.WriteLine(sum1);
Console.WriteLine(sum2);