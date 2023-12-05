using System.Text.Unicode;

Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n').ToList();

long sum1 = 0;
int sum2 = 0;

List<long> seeds = new List<long>();

List<List<Range>> convertionMaps = new List<List<Range>>();

for (int i = 0; i < lines.Count; i++)
{
    string line = lines[i];
    if (line.StartsWith("seeds"))
    {
        seeds = line.Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        i++;
    }
    else if (char.IsLetter(line.First()))
    {
        string destination = line.Split("-", StringSplitOptions.RemoveEmptyEntries)[2].Replace(" map:", "");
        i++;

        List<Range> ranges = new List<Range>();
        while (i < lines.Count && !string.IsNullOrEmpty(lines[i]))
        {
            line = lines[i];
            List<long> digits = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            long destinationRangeStart = digits[0];
            long sourceRangeStart = digits[1];
            long length = digits[2];

            i++;
            ranges.Add(new Range(destinationRangeStart, sourceRangeStart, length));
        }
        convertionMaps.Add(ranges);
    }
}

long currentNum;
foreach (long seed in seeds)
{
    currentNum = seed;
    for(int i = 0; i <= 6; i++)
    {
        List<Range> map = convertionMaps[i];
        foreach(Range range in map)
        {
            if (range.SourceRangeStart < currentNum && currentNum < range.SourceRangeStart + range.RangeLength)
            {
                currentNum=currentNum-range.SourceRangeStart+range.DestinationRangeStart;
            }
        }
    }
    if (sum1 == 0)
    {
        sum1 = currentNum;
    }
    else if (sum1 > currentNum)
    {
        sum1 = currentNum;
    }
}

Console.WriteLine(sum1);
Console.WriteLine(sum2);

class Range
{
    public long DestinationRangeStart, SourceRangeStart, RangeLength;

    public Range(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        DestinationRangeStart = destinationRangeStart;
        SourceRangeStart = sourceRangeStart;
        RangeLength = rangeLength;
    }
}