using System;
using System.Linq;

class B
{
	class LR { public int L, R; }

	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ts = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).Select(x => new LR { L = x[0], R = x[1] + 1 }).OrderBy(x => x.R).ThenBy(x => x.L).ToList();
		var t0 = ts.OrderBy(x => -x.L).ThenBy(x => -x.R).First();

		if (n == 2) { Console.WriteLine(ts.Sum(x => x.R - x.L)); return; }
		var M = Math.Max(0, ts[0].R - t0.L) + ts.Max(x => x.R - x.L);

		ts.Remove(t0);
		for (int l2 = 0, i = 0; i < n - 1; i++)
		{
			var v1 = Math.Max(0, Math.Min(t0.R, i == n - 2 ? int.MaxValue : ts[i + 1].R) - t0.L);
			var v2 = Math.Max(0, ts[0].R - (l2 = Math.Max(l2, ts[i].L)));
			M = Math.Max(M, v1 + v2);
		}
		Console.WriteLine(M);
	}
}
