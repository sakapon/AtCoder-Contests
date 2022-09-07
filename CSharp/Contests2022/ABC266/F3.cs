using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class F3
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

		var u = new bool[n + 1];
		var path = new List<int>();
		Dfs(1, -1);

		bool Dfs(int v, int pv)
		{
			u[v] = true;
			path.Add(v);
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				if (u[nv])
				{
					for (int i = 0; path[i] != nv; i++)
					{
						u[path[i]] = false;
					}
					return true;
				}
				else
				{
					if (Dfs(nv, v)) return true;
				}
			}
			u[v] = false;
			path.RemoveAt(path.Count - 1);
			return false;
		}

		var uf = new UF(n + 1);
		foreach (var e in es)
		{
			if (u[e[0]] && u[e[1]]) continue;
			uf.Unite(e[0], e[1]);
		}

		while (qc-- > 0)
		{
			var (x, y) = Read2();
			sb.AppendLine(uf.AreUnited(x, y) ? "Yes" : "No");
		}
		Console.Write(sb);
	}
}
