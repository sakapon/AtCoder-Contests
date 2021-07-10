using System;
using System.Collections.Generic;
using System.Linq;

class Q088
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var a = Read().Prepend(0).ToArray();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var map = NewArray2<bool>(n + 1, n + 1);
		foreach (var (x, y) in es)
			map[x][y] = true;

		var set = new int[8889][];
		int[] b = null;
		int[] c = null;

		var path = new List<int>();
		for (int v = 1; v <= n; v++)
			if (Dfs(v)) break;

		Console.WriteLine(b.Length);
		Console.WriteLine(string.Join(" ", b));
		Console.WriteLine(c.Length);
		Console.WriteLine(string.Join(" ", c));

		bool Dfs(int v)
		{
			path.Add(v);
			if (CheckSum()) return true;

			for (int nv = v + 1; nv <= n; nv++)
			{
				if (!CheckNext(nv)) continue;
				if (Dfs(nv)) return true;
			}

			path.RemoveAt(path.Count - 1);
			return false;
		}

		bool CheckSum()
		{
			b = path.ToArray();
			var s = b.Sum(i => a[i]);

			if (set[s] == null)
			{
				set[s] = b;
				return false;
			}
			else
			{
				c = set[s];
				return true;
			}
		}

		bool CheckNext(int nv)
		{
			foreach (var v0 in path)
				if (map[v0][nv])
					return false;
			return true;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
