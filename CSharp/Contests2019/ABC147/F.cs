using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	struct LR
	{
		public long L, R;
	}

	static void Main()
	{
		var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
		long n = h[0], x = h[1], d = h[2];

		if (d == 0) { Console.WriteLine(x == 0 ? 1 : n + 1); return; }
		if (d < 0) { d = -d; x = -x; }

		var map = new Dictionary<long, List<LR>>();
		for (long i = 0; i <= n; i++)
		{
			var l = i * (i - 1) / 2;
			var r = l + i * (n - i);

			var a = i * x;
			var m = (a % d + d) % d;
			a = (a - m) / d;

			if (!map.ContainsKey(m)) map[m] = new List<LR>();
			map[m].Add(new LR { L = a + l, R = a + r });
		}

		var c = 0L;
		foreach (var lrs in map.Values)
		{
			var r = long.MinValue;
			foreach (var lr in lrs.OrderBy(s => s.L).Where(s => s.R > r))
			{
				c += lr.R - Math.Max(r, lr.L - 1);
				r = lr.R;
			}
		}
		Console.WriteLine(c);
	}
}
