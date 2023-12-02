Console.WriteLine("Input your file path:");
string path = Console.ReadLine();
string data = File.ReadAllText(path);

data=data.Trim();

string[] lines = data.Split('\n');
int sum1 = 0;
int sum2 = 0;

foreach (string line in lines)
{
    bool possibleGame = true;
    string id = string.Join("", line.Split(":")[0].Where(char.IsDigit).ToList());
    string clearedLine=line.Replace("Game "+id+": ", "");
    string[]sets = clearedLine.Split(";");

    int maxRed = 0;
    int maxGreen = 0;
    int maxBlue = 0;

    foreach (string set in sets)
    {
        string[] cubes = set.Split(",");

        foreach (var cube in cubes)
        {
            string number = string.Join("", cube.Where(char.IsDigit).ToList());
            if (cube.Contains("red"))
            {
                if (int.TryParse(number, out int res))
                {
                    if (res > 12)
                    {
                        possibleGame = false;
                    }
                    if (res > maxRed)
                    {
                        maxRed = res;
                    }
                }
            }
            else if (cube.Contains("green"))
            {
                if (int.TryParse(number, out int res))
                {
                    if (res > 13)
                    {
                        possibleGame = false;
                    }
                    if (res > maxGreen)
                    {
                        maxGreen = res;
                    }
                }
            }
            else if (cube.Contains("blue"))
            {
                if (int.TryParse(number, out int res))
                {
                    if (res>14)
                    {
                        possibleGame=false;
                    }
                    if (res > maxBlue)
                    {
                        maxBlue = res;
                    }
                }
            }
        }
    }
    if (possibleGame)
    {
        sum1 += int.Parse(id);
    }
    int power=maxRed*maxGreen*maxBlue;
    sum2 += power;
}

Console.WriteLine("Part 1: " + sum1);
Console.WriteLine("Part 2: " + sum2);