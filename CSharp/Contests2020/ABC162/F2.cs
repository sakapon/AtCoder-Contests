using System;
using System.Linq;

class F2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var n2 = n / 2;
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		if (n % 2 == 0)
		{
			long M = a.Where((x, i) => i % 2 == 1).Sum(x => (long)x), t = M;
			for (int i = 0; i < n2; i++)
				M = Math.Max(M, t += a[2 * i] - a[2 * i + 1]);
			Console.WriteLine(M);
		}
		else
		{
			var dp = new long[n2 + 1, 3];
			for (int i = 1; i <= n2; i++)
				for (int j = 0; j < 3; j++)
					dp[i, j] = -1L << 60;

			for (int i = 0; i < n2; i++)
				for (int j = 0; j < 3; j++)
					for (int k = j; k < 3; k++)
						dp[i + 1, k] = Math.Max(dp[i + 1, k], dp[i, j] + a[2 * i + k]);
			Console.WriteLine(Enumerable.Range(0, 3).Max(j => dp[n2, j]));
		}
	}
}
