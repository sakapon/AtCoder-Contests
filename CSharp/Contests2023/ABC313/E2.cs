using System;
using System.Linq;

class E2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c - '0').ToArray();

		for (int i = 1; i < n; i++)
			if (s[i - 1] != 1 && s[i] != 1) return -1;

		// dp[i]: s[i..] が 1 文字になるまでの回数
		var r = 0L;

		for (int i = n - 2; i >= 0; i--)
		{
			if (s[i] == 1)
			{
				r++;
				r *= s[i + 1];
			}
			else
			{
				r++;
			}
			r %= M;
		}

		return r;
	}

	const long M = 998244353;
}
