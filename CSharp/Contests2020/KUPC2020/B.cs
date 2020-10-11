using System;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var ps = new int[n].Select(_ => Read()).ToArray();

		const long M = 1000000007;
		var dp = NewArray0<long>(n, k);
		Array.Fill(dp[0], 1);

		for (int i = 1; i < n; i++)
		{
			var a = dp[i - 1];
			var seq = new Seq(a);

			for (int j = 0; j < k; j++)
			{
				var ai = First(0, k, xi => ps[i - 1][xi] > ps[i][j]);
				dp[i][j] = seq.Sum(0, ai) % M;
			}
		}

		Console.WriteLine(Enumerable.Range(0, k).Sum(j => dp[n - 1][j]) % M);
	}

	static T[] NewArray<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}

	static T[][] NewArray0<T>(int n1, int n2) => NewArray(n1, () => new T[n2]);

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}

class Seq
{
	long[] a;
	long[] s;
	public Seq(long[] _a) { a = _a; }

	public long Sum(int minIn, int maxEx)
	{
		if (s == null)
		{
			s = new long[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		}
		return s[maxEx] - s[minIn];
	}
}
