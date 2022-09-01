using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = Read()[0];
		var P = Read();
		var I = Read();

		if (P[0] != 1) return -1;
		var map = ToInverseMap(I, n);

		var L = new int[n + 1];
		var R = new int[n + 1];
		var pi = 0;

		bool Dfs(int l, int r)
		{
			var v = P[pi];
			var m = map[v];
			if (m < l || r <= m) return false;

			if (l < m)
			{
				L[v] = P[++pi];
				if (!Dfs(l, m)) return false;
			}

			if (m + 1 < r)
			{
				R[v] = P[++pi];
				if (!Dfs(m + 1, r)) return false;
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
