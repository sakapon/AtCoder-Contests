using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var sb = new StringBuilder();

		var map = ToListMap(n + 1, es, true);
		var u = new bool[n + 1];

		var q1 = new Stack<(int, int)>();
		q1.Push((-1, 1));
		while (q1.Count > 0)
		{
			var (pv, v) = q1.Pop();
			if (u[v]) continue;
			u[v] = true;
			if (pv != -1) sb.AppendLine($"{pv} {v}");
			foreach (var nv in map[v])
			{
				q1.Push((v, nv));
			}
		}

		u = new bool[n + 1];
		var q2 = new Queue<int>();
		u[1] = true;
		q2.Enqueue(1);
		while (q2.Count > 0)
		{
			var v = q2.Dequeue();
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				u[nv] = true;
				q2.Enqueue(nv);
				sb.AppendLine($"{v} {nv}");
			}
		}

		Console.Write(sb);
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
}
