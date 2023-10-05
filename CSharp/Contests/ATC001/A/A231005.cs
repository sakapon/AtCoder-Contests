using System;
using System.Collections.Generic;

class A231005
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine().ToCharArray());

		var sv = FindCell(s, 's');
		var ev = FindCell(s, 'g');
		var r = ByBFS(s, sv);
		return r[ev];
	}

	public static GridArray<bool> ByBFS(GridArray<char> s, Point sv, char wall = '#')
	{
		var h = s.Height;
		var w = s.Width;
		var u = new GridArray<bool>(h, w);
		var q = new Queue<Point>();
		u[sv] = true;
		q.Enqueue(sv);

		while (q.TryDequeue(out var v))
		{
			foreach (var d in Point.NextsByDelta)
			{
				var nv = v + d;
				if (!nv.IsInRange(h, w)) continue;
				if (s[nv] == wall) continue;
				if (u[nv]) continue;
				u[nv] = true;
				q.Enqueue(nv);
			}
		}
		return u;
	}

	public static Point FindCell(char[][] s, char c)
	{
		var h = s.Length;
		var w = h == 0 ? 0 : s[0].Length;
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				if (s[i][j] == c) return (i, j);
		return (-1, -1);
	}

	public struct Point : IEquatable<Point>
	{
		public static Point Zero = (0, 0);
		public static Point UnitI = (1, 0);
		public static Point UnitJ = (0, 1);

		public int i, j;
		public Point(int i, int j) { this.i = i; this.j = j; }
		public readonly void Deconstruct(out int i, out int j) => (i, j) = (this.i, this.j);
		public override readonly string ToString() => $"{i} {j}";
		public static Point Parse(string s) => Array.ConvertAll(s.Split(), int.Parse);

		public static implicit operator Point(int[] v) => (v[0], v[1]);
		public static explicit operator int[](Point v) => new[] { v.i, v.j };
		public static implicit operator Point((int i, int j) v) => new Point(v.i, v.j);
		public static explicit operator (int, int)(Point v) => (v.i, v.j);

		public readonly bool Equals(Point other) => i == other.i && j == other.j;
		public static bool operator ==(Point v1, Point v2) => v1.Equals(v2);
		public static bool operator !=(Point v1, Point v2) => !v1.Equals(v2);
		public override readonly bool Equals(object obj) => obj is Point v && Equals(v);
		public override readonly int GetHashCode() => (i, j).GetHashCode();

		public static Point operator -(Point v) => new Point(-v.i, -v.j);
		public static Point operator +(Point v1, Point v2) => new Point(v1.i + v2.i, v1.j + v2.j);
		public static Point operator -(Point v1, Point v2) => new Point(v1.i - v2.i, v1.j - v2.j);

		public readonly bool IsInRange(int h, int w) => 0 <= i && i < h && 0 <= j && j < w;
		public readonly Point[] Nexts() => new[] { new Point(i, j - 1), new Point(i, j + 1), new Point(i - 1, j), new Point(i + 1, j) };
		public static readonly Point[] NextsByDelta = new Point[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
	}

	public class GridArray<T>
	{
		readonly T[][] a;
		public T[][] Raw => a;
		public int Height => a.Length;
		public int Width => a.Length == 0 ? 0 : a[0].Length;

		public T this[int i, int j]
		{
			get => a[i][j];
			set => a[i][j] = value;
		}
		public T this[Point p]
		{
			get => a[p.i][p.j];
			set => a[p.i][p.j] = value;
		}

		public GridArray(T[][] raw)
		{
			a = raw;
		}
		public GridArray(int h, int w, T v = default)
		{
			a = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => v));
		}

		public static implicit operator GridArray<T>(T[][] v) => new GridArray<T>(v);
		public static explicit operator T[][](GridArray<T> v) => v.a;
	}
}
