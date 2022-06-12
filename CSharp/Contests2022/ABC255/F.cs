using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var P = Read();
		var I = Read();

		if (P[0] != 1) return -1;
		var map = ToInverseMap(I, n);

		var L = new int[n + 1];
		var R = new int[n + 1];

		var pi = -1;

		bool Dfs(int l, int r)
		{
			var v = P[++pi];
			var ii = map[v];
			if (ii < l || r <= ii) return false;

			if (l < ii)
			{
				L[v] = P[pi + 1];
				if (!Dfs(l, ii)) return false;
			}

			if (ii + 1 < r)
			{
				R[v] = P[pi + 1];
				if (!Dfs(ii + 1, r)) return false;
			}

			return true;
		}

		if (!Dfs(0, n)) return -1;
		return string.Join("\n", Enumerable.Range(1, n).Select(i => $"{L[i]} {R[i]}"));
	}

	public static int[] ToInverseMap(int[] a, int max)
	{
		var d = Array.ConvertAll(new bool[max + 1], _ => -1);
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}
}
