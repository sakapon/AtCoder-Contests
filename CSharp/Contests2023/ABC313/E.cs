using System;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(c => c - '0').ToArray();

		for (int i = 1; i < n; i++)
			if (s[i - 1] != 1 && s[i] != 1) return -1;

		// dp[i]: s[i..] が 0 文字になるまでの回数
		var r = 1L;

		for (int i = n - 2; i >= 0; i--)
		{
			if (s[i] == 1)
			{
				r *= s[i + 1];
				r %= M;
			}
			r++;
		}

		return (r - 1 + M) % M;
	}

	const long M = 998244353;
}
