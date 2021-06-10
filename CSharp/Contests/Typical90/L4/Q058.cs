using System;
using System.Linq;

class Q058
{
	const int M = 100000;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();

		var o = Array.ConvertAll(new bool[M], _ => -1);
		var r = new int[M];
		var period = 0;
		var x = (int)n;

		for (; o[x] == -1; x += x.ToString().Sum(c => c - '0'), x %= M)
		{
			o[x] = period;
			r[period++] = x;
		}

		var si = o[x];
		if (k < si) return r[k];

		period -= si;
		return r[si + (k - si) % period];
	}
}
