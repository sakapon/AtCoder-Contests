using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read().ToList();

		var r = 0L;
		for (int i = 0; i < 3; i++)
		{
			r = Math.Max(r, GetMax(a.ToArray()));

			a.Add(a[0]);
			a.RemoveAt(0);
		}
		return r;
	}

	static long GetMax(int[] a)
	{
		var p = new long[a.Length];
		for (int i = 0; i < a.Length - 2; i++)
			if (IsKadomatsu(a, i)) p[i] = a[i];

		var dp = new long[a.Length];
		for (int i = 0; i < a.Length; i++)
		{
			var max = 0L;
			if (i - 5 >= 0) max = Math.Max(max, dp[i - 5]);
			if (i - 4 >= 0) max = Math.Max(max, dp[i - 4]);
			if (i - 3 >= 0) max = Math.Max(max, dp[i - 3]);
			dp[i] = max + p[i];
		}
		return dp.Max();
	}

	static bool IsKadomatsu(int[] a, int i) => a[i] != a[i + 2] && (a[i] < a[i + 1] && a[i + 1] > a[i + 2] || a[i] > a[i + 1] && a[i + 1] < a[i + 2]);
}
