Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n').ToList();

int sum1 = 1;
int sum2 = 0;

List<int> times = lines[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
List<int> distances = lines[1].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

//Part 1
for(int i=0; i<times.Count; i++)
{
    int ways = 0;
    int time = times[i];
    int distance = distances[i];
    for(int j=1; j<time; j++)
    {
        int timeLeft = time - j;
        int distanceDone = j * timeLeft;
        if (distanceDone >= distance)
        {
            ways++;
        }
    }
    sum1 *= ways;
}

//Part 2
long timeP2 = long.Parse(lines[0].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""));
long distanceP2 = long.Parse(lines[1].Split(":", StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""));

for (long i = 1; i < timeP2; i++)
{
    long timeLeft = timeP2 - i;
    long distanceDone = i * timeLeft;
    if (distanceDone >= distanceP2)
    {
        sum2++;
    }
}

Console.WriteLine(sum1);
Console.WriteLine(sum2);