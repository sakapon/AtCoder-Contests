using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var sb = new StringBuilder();

		// リストと次数を使う方法
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		var deg = new int[n + 1];
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
			deg[e[0]]++;
			deg[e[1]]++;
		}

		var u = new bool[n + 1];
		var q = new Queue<int>();
		for (int v = 1; v <= n; v++)
		{
			if (deg[v] == 1) q.Enqueue(v);
		}

		var uf = new UF(n + 1);
		while (q.TryDequeue(out var v))
		{
			u[v] = true;
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				uf.Unite(v, nv);
				if (--deg[nv] == 1) q.Enqueue(nv);
			}
		}

		while (qc-- > 0)
		{
			var (x, y) = Read2();
			sb.AppendLine(uf.AreUnited(x, y) ? "Yes" : "No");
		}
		Console.Write(sb);
	}
}
