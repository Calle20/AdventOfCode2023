Console.WriteLine("Input your file path:");
List<string> lines = File.ReadAllText(Console.ReadLine()).Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();

int sum1 = 0;
int sum2 = 0;

List<List<Pipe>> tiles= new();

tiles.AddRange(lines.Select(e => e.ToCharArray().Select(ch => new Pipe(ch.ToString())).ToList()));


Pipe start=tiles.Find(row => row.Where(e=>e.Name=="S").Any()).Find(e=>e.Name=="S");
List<Pipe> currentPipes = new() { start };

List<Pipe> lastCurrentPipes=currentPipes;

do
{
    List<Pipe> newCurrentPipes = new();
    foreach (Pipe pipe in currentPipes)
    {
        int row = tiles.FindIndex(row => row.Contains(pipe));
        int column = tiles[row].IndexOf(pipe);

        if (column - 1 >= 0 && pipe == start && tiles[row][column - 1].HasRight)
        {
            Pipe leftPipe = tiles[row][column - 1];
            if (!lastCurrentPipes.Contains(leftPipe))
            {
                newCurrentPipes.Add(leftPipe);
            }
        }
        if (column + 1 < tiles[row].Count && pipe == start && tiles[row][column + 1].HasLeft)
        {
            Pipe rightPipe = tiles[row][column + 1];
            if (!lastCurrentPipes.Contains(rightPipe))
            {
                newCurrentPipes.Add(rightPipe);
            }
        }
        if (row - 1 >= 0 && pipe == start && tiles[row - 1][column].HasBottom)
        {
            Pipe topPipe = tiles[row - 1][column];
            if (!lastCurrentPipes.Contains(topPipe))
            {
                newCurrentPipes.Add(topPipe);
            }
        }
        if (row + 1 < tiles.Count && pipe == start && tiles[row + 1][column].HasTop)
        {
            Pipe bottomPipe = tiles[row + 1][column];
            if (!lastCurrentPipes.Contains(bottomPipe))
            {
                newCurrentPipes.Add(bottomPipe);
            }
        }

        if (column - 1 >= 0 && pipe.HasLeft && tiles[row][column - 1].HasRight)
        {
            Pipe leftPipe = tiles[row][column - 1];
            if (!lastCurrentPipes.Contains(leftPipe))
            {
                newCurrentPipes.Add(leftPipe);
            }
        }
        if (column + 1 < tiles[row].Count && pipe.HasRight && tiles[row][column + 1].HasLeft)
        {
            Pipe rightPipe = tiles[row][column + 1];
            if (!lastCurrentPipes.Contains(rightPipe))
            {
                newCurrentPipes.Add(rightPipe);
            }
        }
        if (row - 1 >= 0 && pipe.HasTop && tiles[row - 1][column].HasBottom)
        {
            Pipe topPipe = tiles[row - 1][column];
            if (!lastCurrentPipes.Contains(topPipe))
            {
                newCurrentPipes.Add(topPipe);
            }
        }
        if (row + 1 < tiles.Count && pipe.HasBottom && tiles[row + 1][column].HasTop)
        {
            Pipe bottomPipe = tiles[row + 1][column];
            if (!lastCurrentPipes.Contains(bottomPipe))
            {
                newCurrentPipes.Add(bottomPipe);
            }
        }
    }
    lastCurrentPipes = currentPipes;
    currentPipes = newCurrentPipes;
    sum1++;
} while (!currentPipes.TrueForAll(e => e.Name == "S"));


Console.WriteLine(sum1-1);
Console.WriteLine(sum2);

class Pipe
{
    public bool HasLeft=false;
    public bool HasRight=false;
    public bool HasTop=false;
    public bool HasBottom=false;
    public string Name;
    public bool Visited=false;

    public Pipe(string name=".")
    {
        Name = name;

        switch (name)
        {
            case "|":
                HasTop = true;
                HasBottom = true;
                break;
            case "-":
                HasLeft = true;
                HasRight = true;
                break;
            case "L":
                HasTop = true;
                HasRight = true;
                break;
            case "J":
                HasTop = true;
                HasLeft = true;
                break;
            case "7":
                HasBottom = true;
                HasLeft = true;
                break;
            case "F":
                HasBottom = true;
                HasRight = true;
                break;
            default:
                HasBottom=false;
                HasLeft=false;
                HasRight=false;
                HasTop=false;
                break;
        }
    }
}