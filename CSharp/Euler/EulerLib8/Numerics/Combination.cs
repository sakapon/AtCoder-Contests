using System;
using System.Collections.Generic;

namespace EulerLib8.Numerics
{
	public static class Combination
	{
		// 階乗も含まれます。
		// 20_P_20 = 20! は Int64 の範囲内です。
		// n を大きい値に変更できます。O(n^2)
		public static long[,] GetNprs()
		{
			var n = 20;
			var dp = new long[n + 1, n + 1];
			for (int i = 0; i <= n; i++)
			{
				dp[i, 0] = 1;
				for (int j = 0; j < i; j++)
					dp[i, j + 1] = dp[i, j] * (i - j);
			}
			return dp;
		}

		// 66_C_33 は Int64 の範囲内です。
		// n を大きい値に変更できます。O(n^2)
		public static long[,] GetNcrs()
		{
			var n = 66;
			var dp = new long[n + 1, n + 1];
			for (int i = 0; i <= n; i++)
			{
				dp[i, 0] = dp[i, i] = 1;
				for (int j = 1; j < i; j++)
					dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
			}
			return dp;
		}
	}
}
