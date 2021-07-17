using System;

class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main()
	{
		var (x, y, z, w) = Read4L();

		var bq = First(0, x, bq_ =>
		{
			var bp_ = bq_ + w;
			return (bp_ * bp_ - y * y) * (bq_ * bq_ - z * z) >= x * x;
		});

		var bp = bq + w;
		var ab = Math.Sqrt(bp * bp - y * y);
		var bc = Math.Sqrt(bq * bq - z * z);

		Console.WriteLine(x - (ab * y + bc * z + (ab - z) * (bc - y)) / 2);
	}

	static double First(double l, double r, Func<double, bool> f, int digits = 9)
	{
		double m;
		while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
		return r;
	}
}
