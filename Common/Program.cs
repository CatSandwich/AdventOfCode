namespace Common
{
    public static class IO
    {
        public static IEnumerable<int> Read(int delimiter)
        {
            while (true)
            {
                var s = Console.ReadLine();
                var i = int.Parse(s);
                if (i == -1) yield break;
                yield return i;
            }
        }

        public static IEnumerable<string> Read(string delimiter)
        {
            while (true)
            {
                var s = Console.ReadLine();
                if (s == delimiter) yield break;
                yield return s;
            }
        }
    }

    public struct V2I
    {
        public int X;
        public int Y;

        public V2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static V2I operator +(V2I lhs, V2I rhs) => new V2I (lhs.X + rhs.X, lhs.Y + rhs.Y);
        public static V2I operator -(V2I lhs, V2I rhs) => new V2I(lhs.X - rhs.X, lhs.Y - rhs.Y);
        public static V2I operator *(V2I lhs, V2I rhs) => new V2I(lhs.X * rhs.X, lhs.Y * rhs.Y);
        public static V2I operator *(V2I lhs, int rhs) => new V2I(lhs.X * rhs, lhs.Y * rhs);
        public static V2I operator *(int lhs, V2I rhs) => rhs * lhs;
        public static bool operator ==(V2I lhs, V2I rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;
        public static bool operator !=(V2I lhs, V2I rhs) => !(lhs == rhs);

        public override string ToString() => $"({X}, {Y})";

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is V2I v2i) return v2i == this;
            return false;
        }
    }
}