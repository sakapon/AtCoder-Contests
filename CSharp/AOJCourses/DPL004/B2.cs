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

		var n2 = n / 2;

		var gs1 = CreateSums(a.Take(n2).ToArray(), r);
		var gs2 = CreateSums(a.Skip(n2).ToArray(), r);

		var c = 0L;
		for (int i1 = 0; i1 <= n2; i1++)
		{
			var i2 = k - i1;
			if (i2 < 0 || n - n2 < i2) continue;

			var s1 = gs1[i1];
			var s2 = gs2[i2];
			s2.Sort();

			foreach (var x1 in s1)
			{
				var li = First(0, s2.Count, si => x1 + s2[si] >= l);
				var ri = First(0, s2.Count, si => x1 + s2[si] > r);
				c += ri - li;
			}
		}
		Console.WriteLine(c);
	}

	static List<long>[] CreateSums(long[] a, long max)
	{
		var n = a.Length;
		var ls = Array.ConvertAll(new bool[n + 1], _ => new List<long>());
		ls[0].Add(0);

		for (int i = 0; i < n; i++)
		{
			for (int j = i; j >= 0; j--)
			{
				var nl = ls[j + 1];
				foreach (var v in ls[j])
				{
					var nv = a[i] + v;
					if (nv <= max) nl.Add(nv);
				}
			}
		}
		return ls;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
