using AlgorithmLab.Collections.Arrays301;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();

		var dq = new ArrayDeque<IntV>(Enumerable.Range(1, n).Select(i => new IntV(i, 0)));
		var r = new List<IntV>();

		while (qc-- > 0)
		{
			var z = Console.ReadLine().Split();
			if (int.Parse(z[0]) == 1)
			{
				var c = z[1];
				var v = dq.First;
				if (c == "R") v += (1, 0);
				else if (c == "L") v -= (1, 0);
				else if (c == "U") v += (0, 1);
				else v -= (0, 1);
				dq.AddFirst(v);
			}
			else
			{
				var p = int.Parse(z[1]) - 1;
				r.Add(dq[p]);
			}
		}

		return string.Join("\n", r);
	}
}

public struct IntV : IEquatable<IntV>
{
	public static IntV Zero = (0, 0);
	public static IntV UnitX = (1, 0);
	public static IntV UnitY = (0, 1);

	public long X, Y;
	public IntV(long x, long y) => (X, Y) = (x, y);
	public void Deconstruct(out long x, out long y) => (x, y) = (X, Y);
	public override string ToString() => $"{X} {Y}";
	public static IntV Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

	public static implicit operator IntV(long[] v) => (v[0], v[1]);
	public static explicit operator long[](IntV v) => new[] { v.X, v.Y };
	public static implicit operator IntV((long x, long y) v) => new IntV(v.x, v.y);
	public static explicit operator (long, long)(IntV v) => (v.X, v.Y);

	public bool Equals(IntV other) => X == other.X && Y == other.Y;
	public static bool operator ==(IntV v1, IntV v2) => v1.Equals(v2);
	public static bool operator !=(IntV v1, IntV v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is IntV v && Equals(v);
	public override int GetHashCode() => (X, Y).GetHashCode();

	public static IntV operator -(IntV v) => (-v.X, -v.Y);
	public static IntV operator +(IntV v1, IntV v2) => (v1.X + v2.X, v1.Y + v2.Y);
	public static IntV operator -(IntV v1, IntV v2) => (v1.X - v2.X, v1.Y - v2.Y);
}
