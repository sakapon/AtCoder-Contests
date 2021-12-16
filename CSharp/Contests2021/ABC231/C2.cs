using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		var r = new int[qc];
		var t = n;

		var es = a.Select(x => (q: false, x))
			.Concat(Enumerable.Range(0, qc).Select(x => (q: true, x)));

		foreach (var (q, x) in es.OrderBy(p => p.q ? qs[p.x] : p.x).ThenBy(p => !p.q))
		{
			if (q)
			{
				r[x] = t;
			}
			else
			{
				t--;
			}
		}

		return string.Join("\n", r);
	}
}
