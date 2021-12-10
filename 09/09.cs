using Common;

var input = IO.Read("exit").Select(line => line.Select(c => c - '0').ToArray()).ToArray().To2D();

var directions = new V2I[]{new(0, 1), new(1, 0), new(0, -1), new(-1, 0)};
bool Valid(V2I pos) => pos.X >= 0 && pos.X < input!.GetLength(0) && pos.Y >= 0 && pos.Y < input.GetLength(1);
IEnumerable<V2I> GetAdjacent(V2I root) => directions.Select(dir => root + dir).Where(Valid);

// ----- PART 1 ----- //
var risk = 0;
for (var x = 0; x < input.GetLength(0); x++)
{
    for (var y = 0; y < input.GetLength(1); y++)
    {
        if (GetAdjacent(new V2I(x, y)).All(point => input[point.X, point.Y] > input[x, y]))
        {
            risk += input[x, y] + 1;
        }
    }
}

Console.WriteLine($"Part 1: {risk}");

// ----- PART 2 ----- //
var basinSizes = new List<int>();
for (var x = 0; x < input.GetLength(0); x++)
{
    for (var y = 0; y < input.GetLength(1); y++)
    {
        if (GetAdjacent(new V2I(x, y)).Any(point => input[point.X, point.Y] < input[x, y]))
        {
            continue;
        }

        var size = 0;
        var tested = new List<V2I>();

        int Recurse(V2I pos)
        {
            if (tested.Contains(pos)) return 0; // If already counted
            if (!Valid(pos)) return 0; // If not in grid
            if (input[pos.X, pos.Y] == 9) return 0; // If a 9

            tested.Add(pos); // Count this square
            var sum = 1; // Includes this one
            foreach (var adj in directions.Select(dir => dir + pos))
            {
                sum += Recurse(adj); // Spread out, counting new squares
            }
            return sum;
        }

        basinSizes.Add(Recurse(new V2I(x, y)));
    }
}

var sum = basinSizes.OrderByDescending(a => a).Take(3).Aggregate(1, (cur, next) => cur * next);
Console.WriteLine($"Part 2: {sum}");