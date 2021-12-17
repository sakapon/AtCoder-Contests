using System;
using System.Collections.Generic;

class E2
{
	class Context
	{
		public int qid, depth, count0;
	}

	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int v = 2; v <= n; v++)
		{
			map[p[v - 2]].Add(v);
		}

		var qmap = Array.ConvertAll(new bool[n + 1], _ => new List<Context>());
		for (int qid = 0; qid < qc; qid++)
		{
			var (u, d) = qs[qid];
			qmap[u].Add(new Context { qid = qid, depth = d });
		}

		// 現在の、深さごとの頂点の個数
		var dcounts = new int[n];

		var r = new int[qc];
		Dfs(1, 0);
		return string.Join("\n", r);

		void Dfs(int v, int depth)
		{
			foreach (var con in qmap[v])
				con.count0 = dcounts[con.depth];

			dcounts[depth]++;

			foreach (var nv in map[v])
				Dfs(nv, depth + 1);

			foreach (var con in qmap[v])
				r[con.qid] = dcounts[con.depth] - con.count0;
		}
	}
}
