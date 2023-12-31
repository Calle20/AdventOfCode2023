﻿using System.Linq;
using System.Data;

Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n').ToList();

long sum1 = 0;
long sum2 = 0;

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

foreach (long seed in seeds)
{
    long currentNum = seed;
    for(int i = 0; i <= 6; i++)
    {
        List<Range> map = convertionMaps[i];
        long newNum = currentNum;
        foreach(Range range in map)
        {
            if ((range.SourceRangeStart < currentNum || range.SourceRangeStart==currentNum) && currentNum < range.SourceRangeStart + range.RangeLength)
            {
                newNum=currentNum-range.SourceRangeStart+range.DestinationRangeStart;
            }
        }
        currentNum=newNum;
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
foreach (long seed in seeds)
{
    long currentNum = calcLocation(seed);
    if (sum1 == 0)
    {
        sum1 = currentNum;
    }
    else if (sum1 > currentNum)
    {
        sum1 = currentNum;
    }
}

Thread thread = new Thread(()=>{
    long maxNum = long.MaxValue;
    for(long i=0;i<maxNum;i++)
    {
        Console.WriteLine(i);
        long seed=calcLocationReverse(i);
        Console.WriteLine(seed);
        if (IsNumberInRanges(seed, seeds.ToArray()))
        {
            sum2 = calcLocation(seed);
            break;
        }
    }
});
thread.Start();
thread.Join();


Console.WriteLine(sum1);
Console.WriteLine(sum2);

static bool IsNumberInRanges(long number, long[] ranges)
{
    for (int i = 0; i < ranges.Length; i += 2)
    {
        long start = ranges[i];
        long length = ranges[i + 1];

        if (number >= start && number < start + length)
        {
            return true;
        }
    }
    return false;
}

long calcLocation(long seed)
{
    long currentNum = seed;
    for (int k = 0; k <= 6; k++)
    {
        List<Range> map = convertionMaps[k];
        long newNum = currentNum;
        foreach (Range range in map)
        {
            if ((range.SourceRangeStart < currentNum || range.SourceRangeStart == currentNum) && currentNum < range.SourceRangeStart + range.RangeLength)
            {
                newNum = currentNum - range.SourceRangeStart + range.DestinationRangeStart;
            }
        }
        currentNum = newNum;
    }
    return currentNum;
}

long calcLocationReverse(long location)
{
    long currentNum = location;
    for (int k = 6; k >= 0; k--)
    {
        List<Range> map = convertionMaps[k];
        long newNum = currentNum;
        foreach (Range range in map)
        {
            if ((range.DestinationRangeStart < currentNum || range.DestinationRangeStart == currentNum) && currentNum < range.DestinationRangeStart + range.RangeLength)
            {
                newNum = currentNum - range.DestinationRangeStart + range.SourceRangeStart;
            }
        }
        currentNum = newNum;
    }
    return currentNum;
}

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