using System;

class C2
{
	const long M = 998244353;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var s = n % M;
		s = s * (s + 1) / 2 % M;
		var r = 0L;

		var c = 9L;
		var u = 0L;
		for (long d = 10; ; d *= 10)
		{
			r += c * u;
			r %= M;

			if (d > n)
			{
				r -= (d - 1 - n) % M * u % M - M;
				r %= M;
				break;
			}

			c = c * 10 % M;
			u = (10 * u + 9) % M;
		}
		return (s - r + M) % M;
	}
}
