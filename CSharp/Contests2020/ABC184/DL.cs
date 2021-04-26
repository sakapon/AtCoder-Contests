using System;

class DL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (a, b, c) = Read3();

		var dp = new MemoDP3<double>(101, 101, 101, -1, (dp, i, j, k) =>
		{
			var all = i + j + k;
			var r = 0D;
			r += (dp[i + 1, j, k] + 1) * i / all;
			r += (dp[i, j + 1, k] + 1) * j / all;
			r += (dp[i, j, k + 1] + 1) * k / all;
			return r;
		});

		for (int i = 0; i <= 100; i++)
			for (int j = 0; j <= 100; j++)
			{
				dp[100, i, j] = 0;
				dp[i, 100, j] = 0;
				dp[i, j, 100] = 0;
			}
		Console.WriteLine(dp[a, b, c]);
	}
}

class MemoDP3<T>
{
	static readonly Func<T, T, bool> TEquals = System.Collections.Generic.EqualityComparer<T>.Default.Equals;
	public T[,,] Raw { get; }
	T iv;
	Func<MemoDP3<T>, int, int, int, T> rec;

	public MemoDP3(int n1, int n2, int n3, T iv, Func<MemoDP3<T>, int, int, int, T> rec)
	{
		Raw = new T[n1, n2, n3];
		for (int i = 0; i < n1; ++i)
			for (int j = 0; j < n2; ++j)
				for (int k = 0; k < n3; ++k)
					Raw[i, j, k] = iv;
		this.iv = iv;
		this.rec = rec;
	}

	public T this[int i, int j, int k]
	{
		get => TEquals(Raw[i, j, k], iv) ? Raw[i, j, k] = rec(this, i, j, k) : Raw[i, j, k];
		set => Raw[i, j, k] = value;
	}
}
