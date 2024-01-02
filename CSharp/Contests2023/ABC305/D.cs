using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		(int l, int r)[] qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var d = new Dictionary<int, int>();
		// 起床: true
		var u = new Dictionary<int, bool>();
		d[0] = 0;
		u[0] = true;

		for (int i = 1; i < n; i++)
		{
			d[a[i]] = d[a[i - 1]];
			if (u[a[i]] = i % 2 == 0) d[a[i]] += a[i] - a[i - 1];
		}

		var pt = 0;
		foreach (var t in a.Concat(qs.Select(q => q.l)).Concat(qs.Select(q => q.r)).OrderBy(x => x))
		{
			if (u.ContainsKey(t))
			{
				pt = t;
			}
			else
			{
				d[t] = d[pt];
				if (!u[pt]) d[t] += t - pt;
			}
		}

		return string.Join("\n", qs.Select(q => d[q.r] - d[q.l]));
	}
}
