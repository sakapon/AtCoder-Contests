using System;
using System.Collections.Generic;
using System.Linq;

class LS
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		// a の値となりうるインデックスのセット
		var map = new Dictionary<int, SortedSet<int>>();
		void AddPair(int i, int v)
		{
			if (!map.ContainsKey(v)) map[v] = new SortedSet<int>();
			map[v].Add(i);
		}

		for (int i = 0; i < n; i++)
		{
			AddPair(i, a[i]);
		}
		foreach (var (t, x, y) in qs)
		{
			if (t == 1) AddPair(x - 1, y);
		}

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

				var r = map[p].GetViewBetween(x, y - 1).Where(i => a[i] == p).Select(i => i + 1).ToArray();
				Console.WriteLine($"{r.Length} " + string.Join(" ", r));
			}
		}
		Console.Out.Flush();
	}
}
