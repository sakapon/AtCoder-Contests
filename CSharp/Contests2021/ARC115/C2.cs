using System;
using System.Linq;

class C2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var a = GetFactorCounts(n);
		Console.WriteLine(string.Join(" ", a[1..].Select(x => x + 1)));
	}

	static int[] GetFactorCounts(int n)
	{
		var d = new int[n + 1];
		for (int p = 2; p * p <= n; ++p)
			if (d[p] == 0)
				for (int x = p * p; x <= n; x += p)
					d[x] = p;

		var c = new int[n + 1];
		for (int x = 2; x <= n; ++x)
			c[x] = d[x] == 0 ? 1 : c[x / d[x]] + 1;
		return c;
	}
}
