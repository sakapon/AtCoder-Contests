using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static long[] Read() => Console.ReadLine().Split().Select(long.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		long n = h[0], x = h[1], m = h[2];

		var f = new long[m];
		for (long i = 0; i < m; i++)
			f[i] = i * i % m;

		var l = new List<long> { 0, x };
		var order = new int[m];
		var c = 0;
		order[x] = ++c;
		var t = x;

		while (order[f[t]] == 0)
		{
			l.Add(l.Last() + f[t]);
			order[f[t]] = ++c;
			t = f[t];
		}
		var o1 = order[f[t]];
		var o2 = ++c;
		if (n < o2) { Console.WriteLine(l[(int)n]); return; }

		n -= o2 - 1;
		var r = l.Last();
		r += n / (o2 - o1) * (l.Last() - l[o1 - 1]);
		r += l[o1 - 1 + (int)(n % (o2 - o1))] - l[o1 - 1];
		Console.WriteLine(r);
	}
}
