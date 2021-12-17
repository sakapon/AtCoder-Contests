using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[2];
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var xs = Array.ConvertAll(ps, p => p.x);
		var ys = Array.ConvertAll(ps, p => p.y);

		var xmap = xs.Distinct().OrderBy(v => v).Select((v, p) => (v, p)).ToDictionary(_ => _.v, _ => _.p);
		var ymap = ys.Distinct().OrderBy(v => v).Select((v, p) => (v, p)).ToDictionary(_ => _.v, _ => _.p);

		return string.Join("\n", Enumerable.Range(0, n).Select(i => $"{xmap[xs[i]] + 1} {ymap[ys[i]] + 1}"));
	}
}
