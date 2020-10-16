using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1];
		var c = Array.ConvertAll(new int[h], _ => Read());

		var dp = NewArray2<int>(h, w);
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				dp[i][j] = 1 - c[i][j];

		for (int i = 1; i < h; i++)
			for (int j = 1; j < w; j++)
				if (c[i][j] == 0 && c[i][j - 1] == 0 && c[i - 1][j] == 0 && c[i - 1][j - 1] == 0)
					dp[i][j] = Math.Min(Math.Min(dp[i - 1][j - 1], dp[i][j - 1]), dp[i - 1][j]) + 1;

		var M = dp.Max(r => r.Max());
		Console.WriteLine(M * M);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => NewArrayF(n1, () => NewArray1(n2, v));
	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}

	static T[] NewArrayF<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}
}
