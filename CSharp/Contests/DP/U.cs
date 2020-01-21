using System;
using System.Linq;

class U
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		var p2 = Enumerable.Range(0, n + 1).Select(i => 1 << i).ToArray();

		var s = new long[p2[n]];
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				for (int f = 0; f < p2[n]; f++)
					if ((f & p2[i]) > 0 && (f & p2[j]) > 0)
						s[f] += a[i][j];

		var dp = (long[])s.Clone();
		for (int f = 0; f < p2[n]; f++)
			for (int g = 1; g <= f; g++)
				if ((f | g) == f)
					dp[f] = Math.Max(dp[f], dp[g] + s[f - g]);
		Console.WriteLine(dp[p2[n] - 1]);
	}
}
