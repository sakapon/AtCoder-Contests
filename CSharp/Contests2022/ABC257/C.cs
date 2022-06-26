using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var w = Read();

		var q = Enumerable.Range(0, n)
			.Select(i => (w: w[i], d: s[i] == '0' ? 1 : -1))
			.GroupBy(p => p.w)
			.OrderBy(g => g.Key)
			.Select(g => g.Sum(p => p.d));

		var t = s.Count(c => c == '1');
		return Math.Max(t, q.Max(d => t += d));
	}
}
