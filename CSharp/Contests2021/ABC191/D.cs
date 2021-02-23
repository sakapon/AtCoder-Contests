using System;

class D
{
	static decimal[] ReadDec() => Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
	static (decimal, decimal, decimal) Read3Dec() { var a = ReadDec(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (X, Y, R) = Read3Dec();

		var l = Math.Ceiling(X - R);
		var r = Math.Floor(X + R);

		var c = 0L;
		for (var x = l; x <= r; x++)
		{
			var R2 = R * R - (x - X) * (x - X);
			var u = Last((long)Math.Floor(Y), (long)Math.Ceiling(Y + R), y => (y - Y) * (y - Y) <= R2);
			var d = First((long)Math.Floor(Y - R), (long)Math.Ceiling(Y), y => (y - Y) * (y - Y) <= R2);
			c += u - d + 1;
		}
		Console.WriteLine(c);
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
