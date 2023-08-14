using System;
using System.Collections.Generic;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, t, m) = Read3();
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var r = 0;
		Partition(n, t, p =>
		{
			foreach (var l in p)
				foreach (var (a, b) in ps)
					if (l.Contains(a - 1) && l.Contains(b - 1)) return;
			r++;
		});
		return r;
	}

	// 区別する n 個の球を、区別しない r 個の箱に入れる
	public static void Partition(int n, int r, Action<List<int>[]> action)
	{
		var p = Array.ConvertAll(new bool[r], _ => new List<int>());
		DFS(0);

		void DFS(int v)
		{
			var t = v + r - n;
			if (t >= 0 && p[t].Count == 0)
			{
				p[t].Add(v);
				if (v == n - 1) action(p);
				else DFS(v + 1);
				p[t].RemoveAt(p[t].Count - 1);
				return;
			}

			var end = false;
			for (int i = 0; !end && i < r; i++)
			{
				if (p[i].Count == 0) end = true;
				p[i].Add(v);
				if (v == n - 1) action(p);
				else DFS(v + 1);
				p[i].RemoveAt(p[i].Count - 1);
			}
		}
	}
}
