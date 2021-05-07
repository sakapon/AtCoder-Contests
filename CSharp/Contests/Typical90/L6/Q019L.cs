using System;
using System.Collections.Generic;

class Q019L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var dp2 = new MemoDP2<int>(2 * n + 1, 2 * n + 1, -1, (dp, l, r) =>
		{
			var v = Math.Abs(a[l] - a[r - 1]) + dp[l + 1, r - 1];

			for (int c = l + 2; c < r; c += 2)
			{
				v = Math.Min(v, dp[l, c] + dp[c, r]);
			}
			return v;
		});

		for (int i = 0; i <= 2 * n; i++)
		{
			dp2[i, i] = 0;
		}
		return dp2[0, 2 * n];
	}
}

class MemoDP2<T>
{
	static readonly Func<T, T, bool> TEquals = EqualityComparer<T>.Default.Equals;
	public T[,] Raw { get; }
	T iv;
	Func<MemoDP2<T>, int, int, T> rec;

	public MemoDP2(int n1, int n2, T iv, Func<MemoDP2<T>, int, int, T> rec)
	{
		Raw = new T[n1, n2];
		for (int i = 0; i < n1; ++i)
			for (int j = 0; j < n2; ++j)
				Raw[i, j] = iv;
		this.iv = iv;
		this.rec = rec;
	}

	public T this[int i, int j]
	{
		get => TEquals(Raw[i, j], iv) ? Raw[i, j] = rec(this, i, j) : Raw[i, j];
		set => Raw[i, j] = value;
	}
}
