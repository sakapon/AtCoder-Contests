using System;
using System.Collections.Generic;
using System.Linq;

class B2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = ReadL();
		int n = (int)h[0], k = (int)h[1];
		long l = h[2], r = h[3];
		var a = ReadL();

		var n1 = n / 2;
		var n2 = n - n1;

		var gs1 = BruteForceHelper.CreateAllSumsForCount(a.Take(n1).ToArray());
		var gs2 = BruteForceHelper.CreateAllSumsForCount(a.Skip(n1).ToArray());

		var c = 0L;
		for (int i1 = 0; i1 <= n1; i1++)
		{
			var i2 = k - i1;
			if (i2 < 0 || n2 < i2) continue;

			var s1 = gs1[i1];
			var s2 = gs2[i2];
			Array.Sort(s2);

			foreach (var x1 in s1)
			{
				var li = First(0, s2.Length, si => x1 + s2[si] >= l);
				var ri = First(0, s2.Length, si => x1 + s2[si] > r);
				c += ri - li;
			}
		}
		Console.WriteLine(c);
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
