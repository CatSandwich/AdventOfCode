var input = Common.IO.Read("exit").ToArray();

var connections = input
    .Select(line => line.Split('-'))
    .SelectMany(arr => new[] { (arr[0], arr[1]), (arr[1], arr[0]) });

var dict = new Dictionary<string, List<string>>();

foreach (var (start, end) in connections)
{
    if (dict.TryGetValue(start, out var list)) list.Add(end);
    else dict[start] = new List<string> { end };
}

bool IsUpper(string s) => s.All(char.IsUpper); 

// Got a little bit lazy for part 2, but it worked and only took about a second or two to run
bool IsOptionValid(string option, string[] visited, bool part2)
{
    if (option == "start") return false; // Can't revisit start
    if (IsUpper(option)) return true; // Large cave
    if (visited.All(cave => cave != option)) return true; // Haven't visited this cave yet
    if (part2 && !visited.Where(cave => !IsUpper(cave)).Any(small => visited.Count(v => v == small) > 1)) return true; // Double small cave
    return false;
}

IEnumerable<string[]> Recurse(string cave, string[]? visited = null, bool part2 = false)
{
    visited ??= new[] { cave };
    if (cave == "end") return new[] { visited };
    return dict[cave]
        .Where(option => IsOptionValid(option, visited, part2))
        .SelectMany(option => Recurse(option, visited.Append(option).ToArray(), part2));
}

// ----- PART 1 ----- //
Console.WriteLine($"Part 1: {Recurse("start").Count()}");
// ----- PART 2 ----- //
Console.WriteLine($"Part 2: {Recurse("start", part2: true).Count()}");