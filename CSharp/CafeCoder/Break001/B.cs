using System;
using System.Linq;

static class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, qc) = Read3();
		var a = Read();
		var b = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var dp = NewArray2<int>(m + 1, n);
		dp[0] = Enumerable.Range(1, n).ToArray();

		for (int j = 0; j < m; j++)
		{
			var map = dp[j].ToInverseMap(n);
			for (int i = 0; i < n; i++)
			{
				dp[j + 1][map[i + 1]] = b[(i + a[j]) % n];
			}
		}

		foreach (var (s, t, x) in qs)
		{
			var i = Array.IndexOf(dp[s - 1], x);
			Console.WriteLine(dp[t][i]);
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	static int[] ToInverseMap(this int[] a, int max)
	{
		var d = Array.ConvertAll(new bool[max + 1], _ => -1);
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}
}
