using System;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var M = 998244353;
		var h = Read();
		var n = h[0];
		var lrs = new int[h[1]].Select(_ => Read()).ToArray();

		var s = 0L;
		var d = new long[n + 1];
		d[1] = 1;
		d[2] = -1;

		for (int i = 1; i <= n; i++)
		{
			s = (s + d[i]) % M;
			foreach (var lr in lrs)
			{
				int l = i + lr[0], r = i + lr[1] + 1;
				if (l <= n) d[l] = (d[l] + s) % M;
				if (r <= n) d[r] = (d[r] - s + M) % M;
			}
		}
		Console.WriteLine(s);
	}
}
