using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();

		var p = new long[n + 1];
		p[0] = 1 - a[0];
		for (int i = 0; i < n; i++)
			p[i + 1] = Math.Min(1L << 60, 2 * p[i] - a[i + 1]);
		p[n] = Math.Min(p[n], 0);
		for (int i = n - 1; i >= 0; i--)
			p[i] = Math.Min(p[i], p[i + 1] + a[i + 1]);

		Console.WriteLine(p.Take(n).Any(x => x <= 0) || p[n] < 0 ? -1 : a.Sum() + p.Sum());
	}
}
