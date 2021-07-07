using System;

class Q082
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var lr = Console.ReadLine().Split();
		var l = long.Parse(lr[0]);
		return MInt(f(lr[1]) - f((l - 1).ToString()));

		long Sum(long n)
		{
			n %= M;
			var r = n * (n + 1) % M;
			return r * MHalf % M;
		}

		long f(string s)
		{
			var v = long.Parse(s);
			if (s.Length == 1) return Sum(v);

			var s9 = new string('9', s.Length - 1);
			var v9 = long.Parse(s9);

			return MInt((Sum(v) - Sum(v9)) * s.Length + f(s9));
		}
	}

	const long M = 1000000007;
	const long MHalf = (M + 1) / 2;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
