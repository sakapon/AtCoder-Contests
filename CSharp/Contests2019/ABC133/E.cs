using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var M = 1000000007;
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var k = h[1];

		var map = new int[h[0] + 1].Select(_ => new List<int>()).ToArray();
		foreach (var r in new int[h[0] - 1].Select(_ => read()))
		{
			map[r[0]].Add(r[1]);
			map[r[1]].Add(r[0]);
		}

		var u = new bool[h[0] + 1];
		var q = new Queue<int>();
		u[1] = true;
		q.Enqueue(1);

		long c = k;
		while (q.Any())
		{
			var p = q.Dequeue();
			c = c * ModNpr(k - (p == 1 ? 1 : 2), map[p].Count - (p == 1 ? 0 : 1), M) % M;

			foreach (var np in map[p])
			{
				if (u[np]) continue;
				u[np] = true;
				q.Enqueue(np);
			}
		}
		Console.WriteLine(c);
	}

	static long ModNpr(int n, int r, int mod)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x = x * ++i % mod) if (i >= n) return x;
	}
}
