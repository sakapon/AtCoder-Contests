using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m, t) = Read3();
		var a = Read();
		var ps = Array.ConvertAll(new bool[m], _ => Read());

		var q = ps.Select(p => (2 * (p[0] - 1), p[1]))
			.Append((0, t))
			.Concat(a.Select((v, i) => (2 * i + 1, -v)))
			.OrderBy(p => p.Item1)
			.ToArray();

		var s = new long[q.Length + 1];
		for (int i = 0; i < q.Length; ++i) s[i + 1] = s[i] + q[i].Item2;

		return s[1..].All(v => v > 0);
	}
}
