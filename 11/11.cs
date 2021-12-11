using Common;

class Octopus
{
    private static Octopus[,] _grid = {};
    private static int _flashCount;

    public byte Energy;
    public bool DoFlash;

    private static void Main()
    {
        var input = IO.Read("exit").Select(line => line.Select(c => (byte)(c - '0')).ToArray()).ToArray();

        // ----- PART 1 ----- //
        _grid = input.Select(line => line.Select(b => new Octopus(b)).ToArray()).ToArray().To2D();
        for (var i = 0; i < 100; i++) Step();
        Console.WriteLine($"Part 1: {_flashCount}");

        // ----- PART 2 ----- //
        _grid = input.Select(line => line.Select(b => new Octopus(b)).ToArray()).ToArray().To2D();
        var step = 1;
        while (!Step()) step++;
        Console.WriteLine($"Part 2: {step}");
    }

    private static bool Step()
    {
        foreach (var octo in _grid) octo.DoFlash = true;
        // First, increase energy level of all octopi by 1
        foreach (var octo in _grid) octo.Energy++;

        // Any octopus with an energy level greater than 9 flashes
        for (var x = 0; x < _grid.GetLength(0); x++)
            for(var y = 0; y < _grid.GetLength(1); y++)
                _grid[x, y].TryFlash(new V2I(x, y));

        // Any octopus that flashed (energy > 9) gets reset to 0
        var allFlashed = true;
        foreach (var octo in _grid)
        {
            if (octo.Energy > 9) octo.Energy = 0;
            else allFlashed = false;
        }
        return allFlashed;
    }

    public Octopus(byte value)
    {
        Energy = value;
    }

    public void TryFlash(V2I position)
    {
        if (!DoFlash) return;
        if (Energy <= 9) return;

        DoFlash = false;
        _flashCount++;

        foreach (var pos in position.GetAdjacent(true).Where(v2 => v2.X >= 0 && v2.Y >= 0 && v2.X < _grid.GetLength(0) && v2.Y < _grid.GetLength(1)))
        {
            _grid[pos.X, pos.Y].Energy++;
            _grid[pos.X, pos.Y].TryFlash(pos);
        }
    }
}