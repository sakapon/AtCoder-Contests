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
		Assign1(n, t, p =>
		{
			foreach (var l in p)
				foreach (var (a, b) in ps)
					if (l.Contains(a - 1) && l.Contains(b - 1)) return false;
			r++;
			return false;
		});
		return r;
	}

	// 区別する n 個の球を、区別しない k 個の箱に入れる
	public static void Assign1(int n, int k, Func<List<int>[], bool> action)
	{
		if (n < k) return;
		var b = Array.ConvertAll(new bool[k], _ => new List<int>());
		DFS(0, 0);

		// i0: 最初の空の箱の番号
		bool DFS(int v, int i0)
		{
			if (v == n) return action(b);

			for (int i = k - i0 < n - v ? 0 : i0; i < k; ++i)
			{
				b[i].Add(v);
				if (DFS(v + 1, i < i0 ? i0 : i0 + 1)) return true;
				b[i].RemoveAt(b[i].Count - 1);
				if (i == i0) break;
			}
			return false;
		}
	}
}
