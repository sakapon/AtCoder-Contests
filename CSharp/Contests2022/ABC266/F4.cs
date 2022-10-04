using System;
using System.Collections.Generic;
using System.Text;

class F4
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var sb = new StringBuilder();

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var u = new int[n + 1];
		Array.Fill(u, -1);
		var path = new List<int>();
		Dfs(1, -1);

		bool Dfs(int v, int pv)
		{
			u[v] = 0;
			path.Add(v);
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				if (u[nv] == 0)
				{
					for (int i = 0; path[i] != nv; i++)
					{
						u[path[i]] = -1;
					}
					return true;
				}
				else
				{
					if (Dfs(nv, v)) return true;
				}
			}
			u[v] = -1;
			path.RemoveAt(path.Count - 1);
			return false;
		}

		for (int v = 1; v <= n; v++)
		{
			if (u[v] != 0) continue;
			Dfs2(v, -1, v);
		}

		void Dfs2(int v, int pv, int root)
		{
			u[v] = root;
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				if (u[nv] != -1) continue;
				Dfs2(nv, v, root);
			}
		}

		while (qc-- > 0)
		{
			var (x, y) = Read2();
			sb.AppendLine(u[x] == u[y] ? "Yes" : "No");
		}
		Console.Write(sb);
	}
}
