var input = Console.ReadLine()!.Split(',').Select(int.Parse).ToArray();

var min = input.Min();
var max = input.Max();

// 1, 3, 6, 10 etc.
int TriangularSequence(int n) => n * (n + 1) / 2;

// ----- PART 1 ----- //
var cost = Enumerable.Range(min, max - min + 1).Select(num => input.Sum(i => Math.Abs(i - num))).Min();
Console.WriteLine($"Part 1: {cost}");

// ----- PART 2 ----- //
cost = Enumerable.Range(min, max - min + 1).Select(num => input.Sum(i => TriangularSequence(Math.Abs(i - num)))).Min();
Console.WriteLine($"Part 2: {cost}");