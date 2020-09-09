using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().Select(x => x == 'R' ? 0 : x == 'G' ? 1 : 2).ToArray();

		var dp = new int[n + 1, 3];
		for (int i = n - 1; i >= 0; i--)
			for (int j = 0; j < 3; j++)
				dp[i, j] = dp[i + 1, j] + (s[i] == j ? 1 : 0);

		var r = 0L;
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
			{
				if (s[i] == s[j]) continue;
				var sk = 3 - s[i] - s[j];
				r += dp[j, sk];
				if (2 * j - i < n && s[2 * j - i] == sk) r--;
			}
		Console.WriteLine(r);
	}
}
