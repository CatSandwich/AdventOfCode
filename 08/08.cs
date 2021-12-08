var data = Common.IO.Read("exit").Select(line =>
{
    var input = line.Split(" | ")[0];
    var output = line.Split(" | ")[1];
    return (input: input.Split(' '), output: output.Split(' '));
}).ToArray();

// ----- PART 1 ----- //
var count = data.Select(i => i.output).Sum(output => output.Count(num => num.Length is 2 or 3 or 4 or 7));
Console.WriteLine($"Part 1: {count}");

// ----- PART 2 ----- //

// Maps numbers to their combination of segments
var numbers = new Dictionary<byte, Segments>
{
    [0] = Segments.Top | Segments.TopLeft | Segments.TopRight |                   Segments.BottomLeft | Segments.BottomRight | Segments.Bottom,
    [1] =                                   Segments.TopRight |                                         Segments.BottomRight                  ,
    [2] = Segments.Top |                    Segments.TopRight | Segments.Middle | Segments.BottomLeft |                        Segments.Bottom,
    [3] = Segments.Top |                    Segments.TopRight | Segments.Middle |                       Segments.BottomRight | Segments.Bottom,
    [4] =                Segments.TopLeft | Segments.TopRight | Segments.Middle |                       Segments.BottomRight                  ,
    [5] = Segments.Top | Segments.TopLeft |                     Segments.Middle |                       Segments.BottomRight | Segments.Bottom,
    [6] = Segments.Top | Segments.TopLeft |                     Segments.Middle | Segments.BottomLeft | Segments.BottomRight | Segments.Bottom,
    [7] = Segments.Top |                    Segments.TopRight |                                         Segments.BottomRight                  ,
    [8] = Segments.Top | Segments.TopLeft | Segments.TopRight | Segments.Middle | Segments.BottomLeft | Segments.BottomRight | Segments.Bottom,
    [9] = Segments.Top | Segments.TopLeft | Segments.TopRight | Segments.Middle |                       Segments.BottomRight | Segments.Bottom
};

// Does the inverse of the above
var numbersInverse = new Dictionary<Segments, byte>();
for (byte i = 0; i < 10; i++) numbersInverse[numbers[i]] = i;

// List to hold mappings of letter to segment position
List<Dictionary<char, Segments>> possibleMappings = new();

// Recursively populates list with all possible mappings
void Permute(char[] current, char[] left)
{
    if (current.Length == 7)
    {
        var dict = new Dictionary<char, Segments>();
        for (var i = 0; i < 7; i++) dict[current[i]] = (Segments)(1 << i);
        possibleMappings.Add(dict);
    }

    foreach (var c in left)
    {
        Permute(current.Append(c).ToArray(), left.Where(val => val != c).ToArray());
    }
}

// Run it
Permute(Array.Empty<char>(), new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });

// Tries all mappings and returns the first that produces only valid numbers
Dictionary<char, Segments> GetMapping(string[] input)
{
    foreach (var mapping in possibleMappings)
    {
        foreach (var num in input)
        {
            var positions = num.Select(c => mapping[c]).ToArray();
            var combo = positions.Aggregate(Segments.None, (cur, next) => cur | next);
            if (!numbersInverse.ContainsKey(combo)) goto end;
        }

        return mapping;

        end: ;
    }

    return null;
}

var answer = 0;

foreach (var (input, output) in data)
{
    var mapping = GetMapping(input);

    // 
    for (var i = 0; i < 4; i++)
    {
        var segments = output[3 - i].Select(c => mapping[c]).Aggregate(Segments.None, (cur, next) => cur | next);

        // Get number with given segments
        var num = numbersInverse[segments];

        // Account for digit position
        answer += num * (int)Math.Pow(10, i);
    }
}

Console.WriteLine($"Part 2: {answer}");

[Flags]
enum Segments : byte
{
    None = 0,
    Top = 1 << 0,
    TopLeft = 1 << 1,
    TopRight = 1 << 2,
    Middle = 1 << 3,
    BottomLeft = 1 << 4,
    BottomRight = 1 << 5,
    Bottom = 1 << 6
}