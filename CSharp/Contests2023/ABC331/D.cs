using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var n2 = n * 2;
		var p = new int[n2, n2];
		for (int i = 0; i < n2; i++)
			for (int j = 0; j < n2; j++)
				if (s[i % n][j % n] == 'B') p[i, j] = 1;

		var rsq = new StaticRSQ2(p);
		return string.Join("\n", new bool[qc].Select(_ => SolveQuery()));

		long SolveQuery()
		{
			var (a, b, c, d) = Read4();
			c++;
			d++;

			var ac = (c - a) / n;
			var bd = (d - b) / n;

			a %= n;
			b %= n;
			c %= n;
			d %= n;

			if (a > c) c += n;
			if (b > d) d += n;

			var r = 0L;
			r += rsq.GetSum(a, b, c, d);
			r += rsq.GetSum(0, 0, n, n) * ac * bd;
			r += rsq.GetSum(a, 0, c, n) * bd;
			r += rsq.GetSum(0, b, n, d) * ac;
			return r;
		}
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
