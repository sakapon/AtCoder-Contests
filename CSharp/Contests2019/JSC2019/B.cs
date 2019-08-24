using System;
using System.Linq;
using System.Numerics;

class B
{
	static void Main()
	{
		var M = 1000000007;
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0];
		BigInteger k = h[1];

		var m1 = new int[n];
		var m2 = new int[n];
		for (int i = 0; i < h[0]; i++)
		{
			for (int j = 0; j < i; j++) if (a[j] < a[i]) m1[i]++;
			for (int j = i + 1; j < n; j++) if (a[j] < a[i]) m2[i]++;
		}
		Console.WriteLine((k * (k - 1) / 2 * m1.Aggregate((x, y) => (x + y) % M) + k * (k + 1) / 2 * m2.Aggregate((x, y) => (x + y) % M)) % M);
	}
}
