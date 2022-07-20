using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x, y, z) = Read4();
		var a = Read();
		var b = Read();

		var r = new List<int>();
		var q = Enumerable.Range(0, n).Select(i => (id: i + 1, a: a[i], b: b[i])).ToArray();

		q = q.OrderBy(p => -p.a).ThenBy(p => p.id).ToArray();
		r.AddRange(q[..x].Select(p => p.id));
		q = q[x..];

		q = q.OrderBy(p => -p.b).ThenBy(p => p.id).ToArray();
		r.AddRange(q[..y].Select(p => p.id));
		q = q[y..];

		q = q.OrderBy(p => -p.a - p.b).ThenBy(p => p.id).ToArray();
		r.AddRange(q[..z].Select(p => p.id));
		q = q[z..];

		r.Sort();
		return string.Join("\n", r);
	}
}
