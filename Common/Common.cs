﻿using System.Security.Cryptography.X509Certificates;

namespace Common
{
    public static class IO
    {
        public static IEnumerable<int> Read(int delimiter)
        {
            while (true)
            {
                var s = Console.ReadLine();
                var i = int.Parse(s!);
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
                yield return s!;
            }
        }
    }

    public static class Extensions
    {
        public static T[,] To2D<T>(this T[][] jagged)
        {
            if (jagged.Length == 0) return new T[0, 0];
            if (jagged[0].Length == 0) return new T[jagged.Length, 0];

            var arr = new T[jagged.Length, jagged[0].Length];
            for (var x = 0; x < jagged.Length; x++)
            {
                for (var y = 0; y < jagged[0].Length; y++)
                {
                    arr[x, y] = jagged[x][y];
                }
            }
            return arr;
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

        public static V2I operator +(V2I lhs, V2I rhs) => new(lhs.X + rhs.X, lhs.Y + rhs.Y);
        public static V2I operator -(V2I lhs, V2I rhs) => new(lhs.X - rhs.X, lhs.Y - rhs.Y);
        public static V2I operator *(V2I lhs, V2I rhs) => new(lhs.X * rhs.X, lhs.Y * rhs.Y);
        public static V2I operator *(V2I lhs, int rhs) => new(lhs.X * rhs, lhs.Y * rhs);
        public static V2I operator *(int lhs, V2I rhs) => rhs * lhs;
        public static bool operator ==(V2I lhs, V2I rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;
        public static bool operator !=(V2I lhs, V2I rhs) => !(lhs == rhs);

        public override string ToString() => $"({X}, {Y})";
        public override bool Equals(object? obj) => obj is V2I v && v == this;
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}