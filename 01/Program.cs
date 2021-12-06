IEnumerable<int> Group(int[] set)
{
    for (var i = 0; i < set.Length - 2; i++)
    {
        yield return set[i] + set[i + 1] + set[i + 2];
    }
}

int GetIncreases(int[] set)
{
    var temp = 0;
    for (var i = 1; i < set.Length; i++)
    {
        if (set[i] > set[i - 1]) temp++;
    }
    return temp;
}

var input = Common.IO.Read(-1).ToArray();

// ----- PART 1 ----- //
Console.WriteLine($"Part 1: {GetIncreases(input)}");

// ----- PART 2 ----- //
var groups = Group(input).ToArray();
Console.WriteLine($"Part 2: {GetIncreases(groups)}");