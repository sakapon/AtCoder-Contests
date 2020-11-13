using System;
using System.Collections.Generic;

class CycleDetection
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		for (int i = 0; i < m; i++)
		{
			var e = es[i];
			map[e[0]].Add(new[] { e[0], e[1], i });
		}

		var u = new bool[n];
		// inEdges でも可。
		var outEdges = new int[n][];

		int Dfs(int v)
		{
			u[v] = true;
			foreach (var e in map[v])
			{
				var nv = e[1];
				if (u[nv])
				{
					if (outEdges[nv] == null) continue;
					outEdges[v] = e;
					return nv;
				}
				else
				{
					outEdges[v] = e;
					var r = Dfs(nv);
					if (r != -1) return r;
					outEdges[v] = null;
				}
			}
			return -1;
		}

		for (int v = 0; v < n; v++)
		{
			if (u[v]) continue;
			var sv = Dfs(v);
			if (sv == -1) continue;

			var path = GetPathEdges(outEdges, sv);
			Console.WriteLine(path.Length);
			foreach (var e in path)
				Console.WriteLine(e[2]);
			return;
		}
		Console.WriteLine(-1);
	}

	static int[][] GetPathEdges(int[][] outEdges, int sv)
	{
		var path = new List<int[]>();
		for (var e = outEdges[sv]; ; e = outEdges[e[1]])
		{
			path.Add(e);
			if (e[1] == sv) break;
		}
		return path.ToArray();
	}
}
