using System;

class Q019
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var dp = NewArray2(2 * n + 1, 2 * n + 1, -1);
		return Rec(0, 2 * n);

		int Rec(int l, int r)
		{
			if (l == r) return 0;
			if (dp[l][r] != -1) return dp[l][r];

			dp[l][r] = Math.Abs(a[l] - a[r - 1]) + Rec(l + 1, r - 1);

			for (int c = l + 2; c < r; c += 2)
			{
				dp[l][r] = Math.Min(dp[l][r], Rec(l, c) + Rec(c, r));
			}
			return dp[l][r];
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
