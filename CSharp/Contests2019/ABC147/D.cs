using System;
using System.Linq;

class D
{
	static void Main()
	{
		var M = 1000000007;
		var t = 1L;
		var p = Enumerable.Range(0, 60).Select(i => i == 0 ? t : t *= 2).ToArray();

		var b = new long[60];
		var n = int.Parse(Console.ReadLine());
		foreach (var x in Console.ReadLine().Split().Select(long.Parse))
			for (int i = 0; i < 60; i++)
				if ((x & p[i]) > 0) b[i]++;
		Console.WriteLine(b.Select((x, i) => x * (n - x) % M * (p[i] % M)).Sum() % M);
	}
}
