using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Common;

var input = IO.Read("exit").ToArray();

var matches = new Dictionary<char, char>()
{
    [')'] = '(',
    [']'] = '[',
    ['}'] = '{',
    ['>'] = '<'
};

var scoring = new Dictionary<char, int>()
{
    [')'] = 3,
    [']'] = 57,
    ['}'] = 1197,
    ['>'] = 25137
};

var scoringPart2 = new Dictionary<char, int>()
{
    ['('] = 1,
    ['['] = 2,
    ['{'] = 3,
    ['<'] = 4
};

(Result result, char? illegal, long? part2) ParseLine(string line)
{
    var stack = new Stack<char>();
    foreach (var c in line)
    {
        // If closing bracket
        if (matches.TryGetValue(c, out var match))
        {
            // If stack empty
            if (!stack.TryPop(out var val)) return (Result.Corrupt, c, null);
            // If popped char doesn't match closing bracket
            if (match != val) return (Result.Corrupt, c, null);
        }
        else
        {
            // Push opening bracket
            stack.Push(c);
        }
    }

    // If stack empty, line is valid
    if(!stack.Any()) return (Result.Valid, null, null);

    // Add up score according to part 2
    var score = 0L;
    while (stack.TryPop(out var c))
    {
        score *= 5;
        score += scoringPart2[c];
    }
    return (Result.Incomplete, null, score);
}

// Run all lines through parser
var results = input.Select(ParseLine).ToArray();

// ----- PART 1 ----- //

// Sum up score for illegal characters
var part1 = results.Where(tup => tup.result == Result.Corrupt).Sum(tup => scoring[tup.illegal!.Value]);
Console.WriteLine($"Part 1: {part1}");

// ----- PART 2 ----- //

// Filter and order by incomplete line scores
var incomplete = results.Where(tup => tup.result == Result.Incomplete).OrderByDescending(tup => tup.part2).ToArray();
// Skip half to get middle score
var part2 = incomplete.Skip(incomplete.Length / 2).First().part2;
Console.WriteLine($"Part 2: {part2}");

enum Result
{
    Valid,
    Incomplete,
    Corrupt
}