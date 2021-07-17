using System;

class Q081
{
	const int max = 5000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var c = new int[max + 1, max + 1];
		foreach (var (a, b) in ps)
		{
			c[a, b]++;
		}
		var rsq = new StaticRSQ2(c);

		var r = 0L;
		for (int i = 0; i + k <= max; i++)
		{
			for (int j = 0; j + k <= max; j++)
			{
				r = Math.Max(r, rsq.GetSum(i, j, i + k + 1, j + k + 1));
			}
		}
		return r;
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
