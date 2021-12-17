using System;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var rn = (long)Math.Sqrt(n);

		var r = 0L;

		for (long x = 1; x <= rn; x++)
		{
			r += n / x;
		}

		var t = rn;
		for (long y = n / rn - 1; y >= 1; y--)
		{
			var q = n / y;
			r += (q - t) * y;
			t = q;
		}

		return r;
	}
}
