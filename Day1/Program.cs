using System.Text.RegularExpressions;

Dictionary<string, string> wordsDigitsDict = new Dictionary<string, string>
{
    {"one", "1"},
    {"two", "2"},
    {"three", "3"},
    {"four", "4"},
    {"five", "5"},
    {"six", "6"},
    {"seven", "7"},
    {"eight", "8"},
    {"nine", "9"}
};

Console.WriteLine("Input your file path:");
string path = Console.ReadLine();
string data=File.ReadAllText(path);

string[] lines= data.Split('\n');
int sum1 = 0;
int sum2 = 0;

foreach (string line in lines)
{
    List<string> numbers1 = new List<string>();
    List<string> numbers2 = new List<string>();

    string pattern1 = @"(?:[1-9])";
    string pattern2 = @"(?=([1-9]|one|two|three|four|five|six|seven|eight|nine))";

    Regex regex1 = new Regex(pattern1);
    Regex regex2 = new Regex(pattern2);

    MatchCollection matches1 = regex1.Matches(line);
    MatchCollection matches2 = regex2.Matches(line);

    foreach (Match match in matches1)
    {
        if (wordsDigitsDict.ContainsKey(match.Value))
        {
            numbers1.Add(wordsDigitsDict[match.Value]);
        }
        else
        {
            numbers1.Add(match.Value);
        }
    }
    foreach (Match match in matches2)
    {
        Console.WriteLine(match.Groups[1].Value);
        if (wordsDigitsDict.ContainsKey(match.Groups[1].Value))
        {
            numbers2.Add(wordsDigitsDict[match.Groups[1].Value]);
        }
        else
        {
            numbers2.Add(match.Groups[1].Value);
        }
    }
    if (numbers1.Count > 0)
    {
        string first=numbers1.ToList().First().ToString();
        string last=numbers1.ToList().Last().ToString();

        string num=first+last;

        sum1 += int.Parse(num);
    }
    if (numbers2.Count > 0)
    {
        string first = numbers2.ToList().First().ToString();
        string last = numbers2.ToList().Last().ToString();

        string num = first + last;

        sum2 += int.Parse(num);
    }
}

Console.WriteLine("Part 1: " + sum1);
Console.WriteLine("Part 2: " + sum2);