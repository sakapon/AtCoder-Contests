using System;
using System.Collections.Generic;
using System.Linq;

class L2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		// キーの値となりうるインデックスのセット
		var map0 = new Dictionary<int, SortedSet<int>>();
		void AddPair(int i, int v)
		{
			if (!map0.ContainsKey(v)) map0[v] = new SortedSet<int>();
			map0[v].Add(i);
		}

		for (int i = 0; i < n; i++)
		{
			AddPair(i, a[i]);
		}
		foreach (var (t, x, y) in qs)
		{
			if (t == 1) AddPair(x - 1, y);
		}

		var map = map0.ToDictionary(p => p.Key, p => p.Value.ToArray());

		var st = new ST1<int>(n, Math.Min, int.MaxValue, a);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var (t, x, y) = q;
			x--;

			if (t == 1)
			{
				a[x] = y;
				st.Set(x, y);
			}
			else
			{
				var p = st.Get(x, y);
				var pis = map[p];
				var jl = First(0, pis.Length, j => pis[j] >= x);
				var jr = First(0, pis.Length, j => pis[j] >= y);

				var r = Array.FindAll(pis[jl..jr], i => a[i] == p);
				Console.WriteLine($"{r.Length} " + string.Join(" ", r.Select(i => i + 1)));
			}
		}
		Console.Out.Flush();
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
