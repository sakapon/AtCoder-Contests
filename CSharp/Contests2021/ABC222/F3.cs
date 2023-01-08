using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.Int.Trees.WeightedTree101;

class F3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read3());
		var d = Read();

		var g = new ListWeightedGraph(2 * n + 1, es, true);
		var dp = new long[n + 1];
		var subs = Array.ConvertAll(dp, _ => new List<(long cost, int v)>());

		DFS1(1, -1);
		DFS2(1, -1, 0);
		return string.Join("\n", dp[1..]);

		// dp, subs: 部分木に対する解
		// 戻り値: v が行先となる場合を含む
		long DFS1(int v, int pv)
		{
			foreach (var (nv, cost) in g.GetEdges(v))
			{
				if (nv == pv) continue;
				var nr = DFS1(nv, v) + cost;
				ChFirstMax(ref dp[v], nr);
				subs[v].Add((nr, nv));
			}
			subs[v].Sort();
			subs[v].Reverse();
			return FirstMax(dp[v], d[v - 1]);
		}

		void DFS2(int v, int pv, long x)
		{
			foreach (var (nv, cost) in g.GetEdges(v))
			{
				if (nv == pv) continue;
				var nx = subs[v][0].v != nv ? subs[v][0].cost : (subs[v].Count > 1 ? subs[v][1].cost : 0);
				ChFirstMax(ref nx, x);
				ChFirstMax(ref nx, d[v - 1]);
				DFS2(nv, v, nx + cost);
			}
			ChFirstMax(ref dp[v], x);
		}
	}

	public static T FirstMax<T>(T o1, T o2) where T : IComparable<T> => o1.CompareTo(o2) < 0 ? o2 : o1;
	public static void ChFirstMax<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) < 0) o1 = o2; }
}
