using System;

class Q082
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (l, r) = Read2L();
		return MInt(f(r) - f(l - 1));

		long Sum(long n)
		{
			n %= M;
			var r = n * (n + 1) % M;
			return r * MHalf % M;
		}

		long f(long v)
		{
			var s = v.ToString();
			if (s.Length == 1) return Sum(v);

			var s9 = new string('9', s.Length - 1);
			var v9 = long.Parse(s9);

			return MInt((Sum(v) - Sum(v9)) * s.Length + f(v9));
		}
	}

	const long M = 1000000007;
	const long MHalf = (M + 1) / 2;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
