using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var amap = new Dictionary<int, int>();
		var bset = new HashSet<int>();

		var ai = new int[n];
		var bi = new int[n];
		Array.Fill(bi, 1 << 30);
		var bmax = new int[n + 1];

		for (int i = 0; i < n; i++)
		{
			var x = a[i];
			if (!amap.ContainsKey(x))
			{
				amap[x] = amap.Count + 1;
			}
			ai[i] = amap.Count;
		}

		for (int i = 0; i < n; i++)
		{
			var x = b[i];
			if (!amap.ContainsKey(x)) break;

			bset.Add(x);
			bi[i] = bset.Count;
			bmax[i + 1] = Math.Max(bmax[i], amap[x]);
		}

		var r = qs.Select(q => ai[q.x - 1] == bi[q.y - 1] && bi[q.y - 1] == bmax[q.y] ? "Yes" : "No");
		return string.Join("\n", r);
	}
}
