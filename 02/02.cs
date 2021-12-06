using Common;

// Split string into direction and amount
(string dir, int amount) Split(string s)
{
    var arr = s.Split(' ');
    return (arr[0], int.Parse(arr[1]));
}

var instructions = IO.Read("exit").Select(Split).ToArray();

// ----- PART 1 ----- //
var position = new V2I(0, 0);
foreach(var instruction in instructions)
{
    position += instruction.amount * instruction.dir switch
    {
        "forward" => new V2I(1, 0),
        "up" => new V2I(0, -1),
        "down" => new V2I(0, 1),
        _ => throw new InvalidOperationException(nameof(instruction.dir))
    };
}
Console.WriteLine($"Part 1: {position.X * position.Y}");

// ----- PART 2 ----- //
var position2 = new V2I(0, 0);
var aim = 0;
foreach(var (dir, amount) in instructions)
{
    aim += dir switch
    {
        "up" => -amount,
        "down" => amount,
        _ => 0
    };

    if(dir == "forward")
    {
        position2.X += amount;
        position2.Y += aim * amount;
    }
}
Console.WriteLine($"Part 2: {position2.X * position2.Y}");