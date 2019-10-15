using System;
using System.Linq;

class F
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = a[0], k = a[1], n1 = (int)Math.Sqrt(n), n2 = n / n1 - 1;

		var w = new int[n1 + n2];
		var vl = w.Length;
		for (int i = 0; i < n1; i++) w[i] = 1;
		for (int i = 1, s = n; i <= n2; i++) s -= (w[vl - i] = s - n / (i + 1));

		Func<long[], long[]> next = x =>
		{
			var t = new long[vl];
			t[vl - 1] = x[0];
			for (var i = 1; i < vl; i++) t[vl - 1 - i] = (t[vl - i] + w[i] * x[i]) % 1000000007;
			return t;
		};

		var v = Enumerable.Repeat(1L, vl).ToArray();
		for (var i = 0; i < k; i++) v = next(v);
		Console.WriteLine(v[0]);
	}
}
