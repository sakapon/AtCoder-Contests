using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Graphs.Trees.Trees2401
{
	class TreeTemplates
	{
		static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
		static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
		static void Main() => Console.WriteLine(Solve());
		static object Solve()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var root = 1;
			var map = ToMap(n + 1, es, true);

			var depths = new int[n];
			var parents = new int[n];
			Array.Fill(depths, -1);
			Array.Fill(parents, -1);
			depths[root] = 0;
			DFS(root, -1);

			return string.Join("\n", qs.Select(q =>
			{
				var (u, v) = q;
				return 0;
			}));

			void DFS(int v, int pv)
			{
				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					depths[nv] = depths[v] + 1;
					parents[nv] = v;
					DFS(nv, v);
				}
			}
		}

		public static int[][] ToMap(int n, int[][] es, bool twoWay)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var e in es)
			{
				map[e[0]].Add(e[1]);
				if (twoWay) map[e[1]].Add(e[0]);
			}
			return Array.ConvertAll(map, l => l.ToArray());
		}

		public static int[][] ToMap(int n, (int, int)[] es, bool twoWay)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				if (twoWay) map[v].Add(u);
			}
			return Array.ConvertAll(map, l => l.ToArray());
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

		public static List<int>[] ToListMap(int n, (int, int)[] es, bool twoWay)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				if (twoWay) map[v].Add(u);
			}
			return map;
		}

		static object Solve2()
		{
			var n = int.Parse(Console.ReadLine());
			var es = Array.ConvertAll(new bool[n - 1], _ => Read2());
			var qc = int.Parse(Console.ReadLine());
			var qs = Array.ConvertAll(new bool[qc], _ => Read2());

			var root = 1;
			var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
			foreach (var (u, v) in es)
			{
				map[u].Add(v);
				map[v].Add(u);
			}

			var path = new List<int>();
			var depths = new int[n];
			var parents = new int[n];
			Array.Fill(depths, -1);
			Array.Fill(parents, -1);
			depths[root] = 0;
			DFS(root);

			return string.Join("\n", qs.Select(q =>
			{
				var (u, v) = q;
				return 0;
			}));

			void DFS(int v)
			{
				foreach (var nv in map[v])
				{
					if (depths[nv] != -1) continue;
					depths[nv] = depths[v] + 1;
					parents[nv] = v;
					DFS(nv);
				}
			}

			bool DFS2(int v, int pv)
			{
				path.Add(v);
				if (v % 2 == 0) return true;

				foreach (var nv in map[v])
				{
					if (nv == pv) continue;
					depths[nv] = depths[v] + 1;
					parents[nv] = v;
					if (DFS2(nv, v)) return true;
				}
				path.RemoveAt(path.Count - 1);
				return false;
			}
		}
	}
}
