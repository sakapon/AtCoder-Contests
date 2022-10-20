using System;
using System.Collections.Generic;
using System.Linq;

// MLE (262 MB 制限)
class C0
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var qs = Array.ConvertAll(ps, p => (x: p[0] + p[1], y: p[1] - p[0] + 3010, d: p[2]));

		var a = new int[6020, 6020];
		foreach (var (x, y, d) in qs)
		{
			a[x, y] += 1;
		}
		var rsq = new StaticRSQ2(a);

		var r = new long[n];
		for (int i = 0; i < n; i++)
		{
			var (x, y, d) = qs[i];
			var x1 = Math.Max(x - d, 0);
			var y1 = Math.Max(y - d, 0);
			var x2 = Math.Min(x + d + 1, 6018);
			var y2 = Math.Min(y + d + 1, 6018);
			r[i] = rsq.GetSum(x1, y1, x2, y2);
		}
		return string.Join("\n", r);
	}
}

public class StaticRSQ2
{
	long[,] s;
	public StaticRSQ2(int[,] a)
	{
		var n1 = a.GetLength(0);
		var n2 = a.GetLength(1);
		s = new long[n1 + 1, n2 + 1];
		for (int i = 0; i < n1; ++i)
		{
			for (int j = 0; j < n2; ++j) s[i + 1, j + 1] = s[i + 1, j] + a[i, j];
			for (int j = 1; j <= n2; ++j) s[i + 1, j] += s[i, j];
		}
	}

	public long GetSum(int l1, int l2, int r1, int r2) => s[r1, r2] - s[l1, r2] - s[r1, l2] + s[l1, l2];
}
