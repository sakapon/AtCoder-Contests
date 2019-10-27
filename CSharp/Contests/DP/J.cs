using System;
using System.Linq;

class J
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).ToLookup(x => x);
		var d = Enumerable.Range(0, 4).Select(i => a[i].Count()).ToArray();
		int s3 = d[3], s2 = s3 + d[2], s1 = s2 + d[1];

		var dp = new double[s3 + 1, s2 + 1, s1 + 1];
		for (int i = 0; i <= s3; i++)
			for (int j = 0; i + j <= s2; j++)
				for (int k = 0, r; (r = i + j + k) <= s1; k++)
					dp[i, j, k] = r == 0 ? 0 : (s1 +
						i * (i > 0 ? dp[i - 1, j + 1, k] : 0) +
						j * (j > 0 ? dp[i, j - 1, k + 1] : 0) +
						k * (k > 0 ? dp[i, j, k - 1] : 0)) / r;
		Console.WriteLine(dp[d[3], d[2], d[1]]);
	}
}
