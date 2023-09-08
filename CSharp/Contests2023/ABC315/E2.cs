using System;
using System.Collections.Generic;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var map = Array.ConvertAll(new bool[n], _ => Read());

		var u = new bool[n + 1];
		var r = new List<int>();
		DFS(1);
		r.RemoveAt(r.Count - 1);
		return string.Join(" ", r);

		void DFS(int v)
		{
			foreach (var nv in map[v - 1][1..])
			{
				if (u[nv]) continue;
				u[nv] = true;
				DFS(nv);
			}
			r.Add(v);
		}
	}
}
