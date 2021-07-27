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

		var vs = a.ToList();
		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				vs.Add(y);
			}
		}
		var vMap = new CompressionHashMap(vs.ToArray());

		// キーの値となりうるインデックスのリスト
		var map0 = Array.ConvertAll(new bool[vMap.Count], _ => new HashSet<int>());
		for (int i = 0; i < n; i++)
		{
			map0[vMap[a[i]]].Add(i);
		}
		foreach (var (t, x, y) in qs)
		{
			if (t == 1)
			{
				map0[vMap[y]].Add(x - 1);
			}
		}

		var map = Array.ConvertAll(map0, l =>
		{
			var b = l.ToArray();
			Array.Sort(b);
			return b;
		});

		var st = new ST1<int>(n, Math.Min, int.MaxValue, a);
		var r = new List<int>();

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
				r.Clear();

				var p = st.Get(x, y);
				var pis = map[vMap[p]];
				var j0 = First(0, pis.Length, j => pis[j] >= x);

				for (int j = j0; j < pis.Length && pis[j] < y; j++)
				{
					if (a[pis[j]] == p)
					{
						r.Add(pis[j] + 1);
					}
				}
				Console.WriteLine($"{r.Count} " + string.Join(" ", r));
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
