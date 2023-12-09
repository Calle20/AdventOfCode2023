Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();

int sum2 = 0;

List<char> navigation = lines[0].ToCharArray().ToList();

Dictionary<string, List<string>> nodes=new Dictionary<string, List<string>>();

foreach (string line in lines.Skip(1)) 
{
    nodes.Add(line.Split(" =", StringSplitOptions.RemoveEmptyEntries)[0], line.Replace("(", "").Replace(")", "").Split("= ", StringSplitOptions.RemoveEmptyEntries)[1].Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList());
}
//Part 1
int steps=getPathLength(navigation, nodes, "AAA", "ZZZ");
Console.WriteLine(steps);

//Part 2
List<string> currentKeys = nodes.Keys.Where(e => e.EndsWith("A")).ToList();
List<long> paths = currentKeys.Select(k => getPathLength(navigation, nodes, k, "Z")).Select(v=>(long) v).ToList();
long p2=paths.Skip(1).Aggregate(paths[0], LCM);
Console.WriteLine(p2);

int getPathLength(List<char> navigation, Dictionary<string,List<string>> nodes, string start, string ending)
{
    string currentKey = start;
    int steps = 0;
    int newNavigationIndex = 0;
    do
    {
        List<string> values = nodes.GetValueOrDefault(currentKey);
        if (navigation[newNavigationIndex] == 'L')
        {
            currentKey = values[0];
        }
        else if (navigation[newNavigationIndex] == 'R')
        {
            currentKey = values[1];
        }
        steps++;
        newNavigationIndex = (newNavigationIndex + 1) % (navigation.Count);
    } while (!currentKey.EndsWith(ending));

    return steps;
}

long GCD(long a, long b)
{
    long remainder;

    while (b != 0)
    {
        remainder = a % b;
        a = b;
        b = remainder;
    }

    return a;
}

long LCM(long a, long b) => (a * b) / GCD(a, b);