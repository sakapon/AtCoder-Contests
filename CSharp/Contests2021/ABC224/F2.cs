using System;
using System.Linq;

class F2
{
	const long M = 998244353;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var n = s.Length;

		long x = s[0];
		long y = 0;
		var p2 = 1L;

		for (int i = 1; i < n; i++)
		{
			p2 = p2 * 2 % M;
			var t = (x * 10 + s[i] * p2) % M;
			var u = (x + y * 2) % M;
			(x, y) = (t, u);
		}
		return (x + y) % M;
	}
}
