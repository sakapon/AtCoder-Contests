using System;
using System.Linq;

class J
{
	static void Main()
	{
		var p2 = Enumerable.Range(0, 18).Select(i => 1 << i).ToArray();
		Console.ReadLine();
		var x = Console.ReadLine().Split().Sum(s => p2[int.Parse(s)]);

		var dp = new double[x + 1];
		for (int f = 1; f <= x; f++)
		{
			var i = Array.FindIndex(p2, p => (f & p) != 0);
			var t = dp[f ^ p2[i]] + 3;
			var c = 1;

			for (int k = 0; k < 2; k++)
				if ((f & p2[++i]) != 0)
				{
					t += dp[f ^ p2[i]];
					++c;
				}

			dp[f] = t / c;
		}
		Console.WriteLine(dp[x]);
	}
}
