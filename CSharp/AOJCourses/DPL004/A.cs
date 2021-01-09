using System;
using System.Collections.Generic;

class A
{
	static long[] Read() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		long n = h[0], v = h[1];
		var a1 = Read();
		var a2 = Read();
		var a3 = Read();
		var a4 = Read();

		var d12 = Comb(n, a1, a2);
		var d34 = Comb(n, a3, a4);

		var r = 0L;
		foreach (var p in d12)
			if (d34.ContainsKey(v - p.Key))
				r += p.Value * d34[v - p.Key];
		Console.WriteLine(r);
	}

	static Dictionary<long, long> Comb(long n, long[] a1, long[] a2)
	{
		var d = new Dictionary<long, long>();
		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
			{
				var s = a1[i] + a2[j];
				if (d.ContainsKey(s)) d[s]++;
				else d[s] = 1;
			}
		return d;
	}
}
