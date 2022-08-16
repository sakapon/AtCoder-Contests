using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var sb = new StringBuilder();

		Permutation(Enumerable.Range(1, m).ToArray(), n, p =>
		{
			if (IsMonotonic(p)) sb.AppendLine(string.Join(" ", p));
		});

		Console.Write(sb);
	}

	static bool IsMonotonic(int[] p)
	{
		for (int i = 1; i < p.Length; i++)
		{
			if (p[i - 1] >= p[i]) return false;
		}
		return true;
	}

	public static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var p = new T[r];
		var u = new bool[values.Length];

		Action<int> Dfs = null;
		Dfs = i =>
		{
			for (int j = 0; j < values.Length; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;
				if (i + 1 < r) Dfs(i + 1); else action(p);
				u[j] = false;
			}
		};
		if (r > 0) Dfs(0); else action(p);
	}
}
