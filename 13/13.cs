using Common;

var input = string.Join('\n', IO.Read("exit").ToArray());

var points = input
    .Split("\n\n")[0]
    .Split('\n')
    .Select(line => new V2I(int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1])))
    .ToArray();

var folds = input
    .Split("\n\n")[1]
    .Split("\n")
    .Select(line => line["fold along ".Length..].Split('='))
    .Select(split => new {axis = split[0], value = int.Parse(split[1])})
    .ToArray();

V2I Transform(V2I vec, string axis, int value)
{
    if (axis == "y") return vec.Y > value ? new V2I(vec.X, value - (vec.Y - value)) : vec;
    return vec.X > value ? new V2I(value - (vec.X - value), vec.Y) : vec;
}

var counts = new List<int>();

for (var i = 0; i < folds.Length; i++)
{
    points = points
        //.Where(point => (fold.axis == "x" ? point.Y : point.X) != fold.value)
        .Select(point => Transform(point, folds[i].axis, folds[i].value))
        .Distinct()
        .ToArray();

    if (i == folds.Length - 1)
    {
        for (var y = 0; y <= points.Max(v2 => v2.Y); y++)
        {
            for (var x = 0; x <= points.Max(v2 => v2.X); x++)
            {
                Console.Write(points.Any(v2 => v2 == new V2I(x, y)) ? '█' : ' ');
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    counts.Add(points.Length);
}

Console.WriteLine($"Part 1: {counts[0]}");
Console.WriteLine($"Part 2: Read the text above.");