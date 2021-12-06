using Common;

// Parse bingo sequence and boards. Find winner for part 1, find last bingo for part 2

var input = string.Join("\n", IO.Read("exit").ToArray()).Split("\n\n");
var sequence = input[0].Split(',').Select(int.Parse).ToArray();
var boards = input[1..].Select(board => board.Split('\n')).ToArray();

int[] SplitParse(string line) => line.Split(' ').Where(str => str != "").Select(int.Parse).ToArray();
int[][] ParseBoard(string[] lines) => lines.Select(SplitParse).ToArray();

var parsedBoards = boards.Select(ParseBoard).Select(board => new Board(board)).ToArray();

(Board? board, int last) Run(IEnumerable<Board> boards, bool getLast)
{
    foreach (var i in sequence)
    {
        foreach (var board in boards)
        {
            if (!board.NumPositions.TryGetValue(i, out var v2i)) continue;

            board.Values[v2i.X, v2i.Y] = true;

            if (board.IsBingo())
            {
                if (!getLast) return (board, i);
                if (boards.Any(b => !b.IsBingo())) continue;
                return (board, i);
            }
        }
    }
    return (null, sequence.Last());
}

// ----- PART 1 ----- //
var winner = Run(parsedBoards.ToArray(), false);
Console.WriteLine($"Part 1: {(winner.board?.SumUnmarked() * winner.last).ToString() ?? "No winner."}");

// ----- PART 2 ----- //
var loser = Run(parsedBoards.ToArray(), true);
Console.WriteLine($"Part 2: {(loser.board?.SumUnmarked() * loser.last).ToString() ?? "No winner."}");

class Board
{
    public static readonly V2I[][] Lines =
    {
        new V2I[]{new(0, 0), new(0, 1), new(0, 2), new(0, 3), new(0, 4)},
        new V2I[]{new(1, 0), new(1, 1), new(1, 2), new(1, 3), new(1, 4)},
        new V2I[]{new(2, 0), new(2, 1), new(2, 2), new(2, 3), new(2, 4)},
        new V2I[]{new(3, 0), new(3, 1), new(3, 2), new(3, 3), new(3, 4)},
        new V2I[]{new(4, 0), new(4, 1), new(4, 2), new(4, 3), new(4, 4)},

        new V2I[]{new(0, 0), new(1, 0), new(2, 0), new(3, 0), new(4, 0)},
        new V2I[]{new(0, 1), new(1, 1), new(2, 1), new(3, 1), new(4, 1)},
        new V2I[]{new(0, 2), new(1, 2), new(2, 2), new(3, 2), new(4, 2)},
        new V2I[]{new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3)},
        new V2I[]{new(0, 4), new(1, 4), new(2, 4), new(3, 4), new(4, 4)}
    };

    public Dictionary<int, V2I> NumPositions = new();
    public bool[,] Values = new bool[5, 5];

    public Board(int[][] nums)
    {
        for(var x = 0; x < 5; x++)
        {
            for(var y = 0; y < 5; y++)
            {
                Values[x, y] = false;
                NumPositions[nums[x][y]] = new V2I(x, y);
            }
        }
    }

    // If any line has all points set, bingo
    public bool IsBingo() => Lines.Any(line => line.All(point => Values[point.X, point.Y]));

    public int SumUnmarked()
    {
        return NumPositions.Where(kvp => !Values[kvp.Value.X, kvp.Value.Y]).Select(kvp => kvp.Key).Sum();
    }

    // Was used for debugging, no longer needed but I'll leave it
    public override string ToString()
    {
        var sb = "";
        for(var x = 0; x < 5; x++)
        {
            for(var y = 0; y < 5; y++)
            {
                sb += NumPositions.First(kvp => kvp.Value == new V2I(x, y)).Key.ToString().PadLeft(2, ' ') + ' ';
            }
            sb += '\n';
        }
        sb += '\n';
        for(var x = 0; x < 5; x++)
        {
            for(var y = 0; y < 5; y++)
            {
                sb += Values[x, y] ? "x " : "o ";
            }
            sb += '\n';
        }

        return sb;
    }
}