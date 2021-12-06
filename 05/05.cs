using Common;

var dict = new Dictionary<V2I, bool?>();

var rays = IO.Read("exit").Select(line => new Ray(line)).ToArray();

var min = new V2I(rays.Select(r => r.Start.X).Min(), rays.Select(r => r.Start.Y).Min());
var max = new V2I(rays.Select(r => r.Start.X).Max(), rays.Select(r => r.Start.Y).Max());

int Run(bool diag)
{
    var count = 0;
    for (var x = min.X; x <= max.X; x++)
    {
        for (var y = min.Y; y <= max.Y; y++)
        {
            var v2i = new V2I(x, y);
            var c = 0;
            foreach (var ray in rays)
            {
                var b = ray.Hits(v2i, diag);
                if (b) c++;
                if (c >= 2)
                {
                    count++;
                    break;
                }
            }
        }
    }
    return count;
}

// ----- PART 1 ----- //
Console.WriteLine($"Part 1: {Run(false)}");

// ----- PART 2 ----- //
Console.WriteLine($"Part 2: {Run(true)}");

class Ray
{
    public V2I Start;
    public V2I End;

    public Ray(string line)
    {
        var points = line.Split(" -> ");
        var start = points[0].Split(',');
        var end = points[1].Split(',');
        Start = new V2I(int.Parse(start[0]), int.Parse(start[1]));
        End = new V2I(int.Parse(end[0]), int.Parse(end[1]));
    }

    public Ray((int x, int y) start, (int x, int y) end)
    {
        Start = new V2I(start.x, start.y);
        End = new V2I(end.x, end.y);
    }

    public bool Hits(V2I point, bool diag)
    {
        if(Start.X == End.X)
        {
            if (point.X != Start.X) return false;
            var min = Start.Y < End.Y ? Start.Y : End.Y;
            var max = Start.Y > End.Y ? Start.Y : End.Y;
            return point.Y >= min && point.Y <= max;
        }
        else if(Start.Y == End.Y) 
        {
            if (point.Y != Start.Y) return false;
            var min = Start.X < End.X ? Start.X : End.X;
            var max = Start.X > End.X ? Start.X : End.X;
            return point.X >= min && point.X <= max;
        }
        else if(diag)
        {
            V2I step = new V2I(End.X > Start.X ? 1 : -1, End.Y > Start.Y ? 1 : -1);

            var minX = Start.X < End.X ? Start.X : End.X;
            var maxX = Start.X > End.X ? Start.X : End.X;
            var minY = Start.Y < End.Y ? Start.Y : End.Y;
            var maxY = Start.Y > End.Y ? Start.Y : End.Y;
            if (point.X < minX || point.X > maxX) return false;
            if (point.Y < minY || point.Y > maxY) return false;

            var diff = point - Start;
            diff *= step;
            return diff.X == diff.Y;
        }
        return false;
    }
}