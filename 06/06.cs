// Caches how many fish a fish of a given step will become in a given number of iterations
Dictionary<(byte step, int iterations), long> cache = new();

// Iterate a fish of a given step one time
IEnumerable<byte> Iterate(byte step)
{
    if (step == 0)
    {
        yield return 8;
        yield return 6;
    }
    else yield return (byte)(step - 1);
}

// Get the number of fish created by a fish of a given step in a given number of iterations
long RunOne(byte step, int times)
{
    if (times <= step) return 1; // Won't duplicate
    if (cache.TryGetValue((step, times), out var val)) return val; // Cached value
    
    // Iterate once then recurse on results
    return cache[(step, times)] = Iterate(step).Select(i => RunOne(i, times - 1)).Sum();
}

long Run(IEnumerable<byte> input, int times) => input.Select(i => RunOne(i, times)).Sum();

// ----- INPUT  ----- //
var seq = Console.ReadLine()!.Split(',').Select(byte.Parse).ToArray();
// ----- PART 1 ----- //
Console.WriteLine($"Part 1: {Run(seq, 80)}");
// ----- PART 2 ----- //
Console.WriteLine($"Part 2: {Run(seq, 256)}");