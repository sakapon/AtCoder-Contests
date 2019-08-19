using System;
using System.Linq;

class F
{
	static void Main()
	{
		var M = 1000000007;
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = a[0], k = a[1];

		var c = new int[n + 1];
		int j = 1, m = n / 2;
		for (; j <= m; j++) c[j] = n / j;
		for (; j <= n; j++) c[j] = 1;

		var v = c.ToArray();
		var d = new int[n + 1];
		for (var i = 3; i <= k; i++)
		{
			for (j = 1; j <= n; j++) d[j] = (d[j - 1] + v[j]) % M;
			for (j = 1; j <= n; j++) v[j] = d[c[j]];
		}
		Console.WriteLine(v.Aggregate((x, y) => (x + y) % M));
	}
}
