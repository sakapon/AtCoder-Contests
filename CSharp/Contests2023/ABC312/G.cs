using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		// 親を含む (無向)
		var map0 = ToListMap(n + 1, es, true);

		// 親を含まない (有向)
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		var descs = new long[n + 1];
		DFS(1, -1);

		var r = 0L;
		for (int v = 1; v <= n; v++)
		{
			var cs = map[v].Select(nv => descs[nv]).Prepend(n - descs[v]).ToArray();
			r += Comb3(cs);
		}
		return r;

		void DFS(int v, int pv)
		{
			foreach (var nv in map0[v])
			{
				if (nv == pv) continue;
				map[v].Add(nv);
				DFS(nv, v);
				descs[v] += descs[nv];
			}
			descs[v]++;
		}
	}

	public static List<int>[] ToListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return map;
	}

	// 3 つの箱から区別できるボールを 1 つずつ取り出す方法の数
	public static long Comb3(long[] a)
	{
		var (s1, s2, r) = (0L, 0L, 0L);
		for (int i = 2; i < a.Length; i++)
		{
			s1 += a[i - 2];
			s2 += s1 * a[i - 1];
			r += s2 * a[i];
		}
		return r;
	}
}
