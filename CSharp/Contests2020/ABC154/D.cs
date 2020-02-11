using System;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var p = Read();

		int t = p.Take(k).Sum(), M = t;
		for (int i = 0; i < n - k; i++)
			M = Math.Max(M, t += p[i + k] - p[i]);
		Console.WriteLine((M + k) / 2.0);
	}
}
