using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var xs = ps.SelectMany(p => new[] { (x: p.a, d: 1), (x: p.a + p.b, d: -1) })
			.GroupBy(p => p.x)
			.OrderBy(g => g.Key)
			.Select(g => (x: g.Key, d: g.Sum(p => p.d)))
			.ToArray();

		var r = new long[n + 1];
		var t = 0;

		for (int i = 0; i < xs.Length - 1; i++)
		{
			var (x, d) = xs[i];
			r[t += d] += xs[i + 1].x - x;
		}
		return string.Join(" ", r[1..]);
	}
}
