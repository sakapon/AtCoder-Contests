using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(long.Parse).ToArray();

		var p2 = new long[60];
		p2[0] = 1;
		for (int i = 1; i < 60; i++) p2[i] = 2 * p2[i - 1];

		var f = new long[60];
		foreach (var x in a)
			for (int i = 0; i < 60; i++)
				if ((x & p2[i]) > 0) f[i]++;

		var s = a.Aggregate((x, y) => (x + y) % M) * (a.Length - 1) % M;
		var fs = f.Select((x, i) => x * (x - 1) / 2 % M * (p2[i] % M) % M).Aggregate((x, y) => (x + y) % M);
		Console.WriteLine(MInt(s - 2 * fs));
	}

	const int M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
