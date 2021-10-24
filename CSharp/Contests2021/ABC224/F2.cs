using System;

class F2
{
	const long M = 998244353;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var (x, y, p) = (0L, 0L, 1L);

		for (int i = 0; i < n; i++)
		{
			y = (x + y * 2) % M;
			x = (x * 10 + (s[i] - '0') * p) % M;
			p = p * 2 % M;
		}
		return (x + y) % M;
	}
}
