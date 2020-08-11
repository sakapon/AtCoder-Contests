using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();

		var p = a.Select((x, i) => (1L << Math.Min(60, i)) - x).ToArray();
		if (p.Take(n).Any(x => x <= 0) || p.Last() < 0) { Console.WriteLine(-1); return; }

		p[n] = 0;
		for (int i = n - 1; i >= 0; i--)
			p[i] = Math.Min(p[i], p[i + 1] + a[i + 1]);
		for (int i = 1; i <= n; i++)
			p[i] = Math.Min(p[i], 2 * p[i - 1] - a[i]);

		if (p.Take(n).Any(x => x <= 0) || p.Last() < 0) { Console.WriteLine(-1); return; }
		Console.WriteLine(a.Sum() + p.Sum());
	}
}
