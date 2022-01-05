using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var u = q[1];
			var v = q[2];

			if (q[0] == 1)
			{
				if (map[u].Contains(v)) map[u].Remove(v);
				else map[u].Add(v);

				if (map[v].Contains(u)) map[v].Remove(u);
				else map[v].Add(u);
			}
			else
			{
				WriteYesNo(Dfs(n + 1, v => map[v].ToArray(), u, v)[v]);
			}
		}
		Console.Out.Flush();
	}

	public static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var u = new bool[n];
		var q = new Stack<int>();
		u[sv] = true;
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in nexts(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == ev) return u;
				q.Push(nv);
			}
		}
		return u;
	}
}
