using System;

class D2
{
	static int[] Read() => Array.ConvertAll(("0 " + Console.ReadLine()).Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();
		var a = Read();

		var t = 1;
		var u = new bool[n + 1];
		u[t] = true;

		while (true)
		{
			t = a[t];
			if (--k == 0) return t;
			if (u[t]) break;
			u[t] = true;
		}

		var (start, period) = (t, 0);
		while (true)
		{
			t = a[t];
			period++;
			if (--k == 0) return t;
			if (t == start) break;
		}

		if ((k %= period) == 0) return t;
		while (true)
		{
			t = a[t];
			if (--k == 0) return t;
		}
	}
}
