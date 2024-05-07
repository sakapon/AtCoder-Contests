using System;
using System.Collections.Generic;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int d) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var root = 1;
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < n - 1; i++)
			map[p[i]].Add(i + 2);

		var qmap = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int qi = 0; qi < qc; qi++)
			qmap[qs[qi].u].Add(qi);

		// 現在の、深さごとの頂点の個数
		var dcounts = new int[n];
		var dcounts_q = new int[qc];

		var r = new int[qc];
		DFS(root, 0);
		return string.Join("\n", r);

		void DFS(int v, int depth)
		{
			foreach (var qi in qmap[v])
				dcounts_q[qi] = dcounts[qs[qi].d];

			dcounts[depth]++;

			foreach (var nv in map[v])
				DFS(nv, depth + 1);

			foreach (var qi in qmap[v])
				r[qi] = dcounts[qs[qi].d] - dcounts_q[qi];
		}
	}
}
