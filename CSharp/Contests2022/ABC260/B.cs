using System;
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

		var q = Enumerable.Range(0, n).Select(i => (id: i + 1, a: a[i], b: b[i])).ToArray();

		q = q.OrderBy(p => -p.a).ThenBy(p => p.id).ToArray();
		q[x..].OrderBy(p => -p.b).ThenBy(p => p.id).ToArray().CopyTo(q, x);
		q[(x + y)..].OrderBy(p => -p.a - p.b).ThenBy(p => p.id).ToArray().CopyTo(q, x + y);

		return string.Join("\n", q[..(x + y + z)].Select(p => p.id).OrderBy(x => x));
	}
}
