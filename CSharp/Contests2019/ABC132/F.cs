using System;
using System.Linq;
using System.Numerics;

class F
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = a[0], k = a[1];

		var w = Enumerable.Range(1, n).GroupBy(i => n / i).Select(g => g.Count()).ToArray();
		var vl = w.Length;

		Func<BigInteger[], BigInteger[]> next = x =>
		{
			var t = new BigInteger[vl];
			t[vl - 1] = x[0];
			for (int i = 1; i < vl; i++) t[vl - 1 - i] = t[vl - i] + w[i] * x[i];
			return t;
		};

		var v = Enumerable.Repeat(BigInteger.One, vl).ToArray();
		for (var i = 0; i < k; i++) v = next(v);
		Console.WriteLine(v[0] % 1000000007);
	}
}
