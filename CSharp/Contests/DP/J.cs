using System;
using System.Linq;

class J
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToLookup(x => x);
		var d = Enumerable.Range(0, 4).Select(i => a[i].Count()).ToArray();
		var s = Enumerable.Range(0, 4).Select(i => d.Skip(i).Sum()).ToArray();

		var dp = new double[s[3] + 1, s[2] + 1, s[1] + 1];
		for (int i = 0; i <= s[3]; i++)
			for (int j = 0; i + j <= s[2]; j++)
				for (int k = 0, r; (r = i + j + k) <= s[1]; k++)
					dp[i, j, k] = r == 0 ? 0 : (n +
						i * (i > 0 ? dp[i - 1, j + 1, k] : 0) +
						j * (j > 0 ? dp[i, j - 1, k + 1] : 0) +
						k * (k > 0 ? dp[i, j, k - 1] : 0)) / r;
		Console.WriteLine(dp[d[3], d[2], d[1]]);
	}
}
