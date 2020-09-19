using System;

class C2
{
	static void Main()
	{
		var q = int.Parse(Console.ReadLine());

		for (int k = 0; k < q; k++)
		{
			var s = Console.ReadLine();
			var t = Console.ReadLine();
			var n = s.Length;
			var m = t.Length;

			var dp = NewArray0<int>(n + 1, m + 1);
			for (int i = 0; i < n; i++)
				for (int j = 0; j < m; j++)
					dp[i + 1][j + 1] = s[i] == t[j] ? dp[i][j] + 1 : Math.Max(dp[i + 1][j], dp[i][j + 1]);
			Console.WriteLine(dp[n][m]);
		}
	}

	static T[] NewArray<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}

	static T[][] NewArray0<T>(int n1, int n2) => NewArray(n1, () => new T[n2]);
}
