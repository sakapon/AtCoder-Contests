using System;
using System.Numerics;

class F
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		long l = h[0], a = h[1], b = h[2], m = h[3];

		BigInteger r = 0, td = 1;
		for (int d = (a + (l - 1) * b).ToString().Length; d >= a.ToString().Length; d--)
		{
			var d10 = (long)BigInteger.Pow(10, d);
			var n_max = Math.Min(l - 1, (d10 - 1 - a) / b);
			var t_min = d10 / 10 - 1 - a;
			var n_min_ex = t_min < 0 ? -1 : t_min / b;

			long c = a + n_max * b, n = n_max - n_min_ex;
			var d10n = BigInteger.Pow(d10, (int)n);

			var s = (d10n - 1) / (d10 - 1) * c - ((n - 1) * d10n * d10 - n * d10n + d10) / BigInteger.Pow(d10 - 1, 2) * b;
			r = (r + td * s) % m;
			td = td * (d10n % m) % m;
		}
		Console.WriteLine(r);
	}
}
