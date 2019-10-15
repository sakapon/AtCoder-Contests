using System;
using System.Linq;

class E
{
	struct R
	{
		public int A, B, C;
	}

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var rs = new int[h[1]].Select(_ => read()).Select(r => new R { A = r[0], B = r[1], C = r[2] - h[2] }).ToArray();
		var map = rs.ToLookup(r => r.A);

		var c = Enumerable.Repeat(long.MinValue, n + 1).ToArray();
		c[1] = 0;
		for (long v, i = 0; i < n; i++)
			foreach (var r in rs)
				if (c[r.A] != c[0] && c[r.B] < (v = c[r.A] + r.C)) c[r.B] = v;

		var u = new bool[n + 1];
		foreach (var r in rs)
			if (c[r.A] != c[0] && c[r.B] < c[r.A] + r.C) Find(r.B, map, u);
		Console.WriteLine(u[n] ? -1 : Math.Max(c[n], 0));
	}

	static void Find(int p, ILookup<int, R> map, bool[] u)
	{
		u[p] = true;
		foreach (var r in map[p]) if (!u[r.B]) Find(r.B, map, u);
	}
}
