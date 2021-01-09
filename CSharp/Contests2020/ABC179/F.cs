using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var qs = new int[h[1]].Select(_ => Read()).ToArray();

		var r = (long)(n - 2) * (n - 2);

		// i -> j
		var l1 = new List<(int i, int j)> { (n, n) };
		// j -> i
		var l2 = new List<(int j, int i)> { (n, n) };

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var li = Last(0, l2.Count - 1, x => l2[x].j > q[1]);
				var i = l2[li].i;
				r -= i - 2;
				if (q[1] < l1.Last().j) l1.Add((i, q[1]));
			}
			else
			{
				var li = Last(0, l1.Count - 1, x => l1[x].i > q[1]);
				var j = l1[li].j;
				r -= j - 2;
				if (q[1] < l2.Last().i) l2.Add((j, q[1]));
			}
		}
		Console.WriteLine(r);
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
