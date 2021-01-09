﻿using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var a = new int[4].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var sv = 0L;
		for (int i = 0; i < 4; i++)
			for (int j = 0; j < 4; j++)
				sv = (sv << 4) + a[i][j];

		var gp = new P(3, 3);
		var gv = 0L;
		var ga = Enumerable.Range(1, 16).ToArray();
		ga[15] = 0;
		for (int i = 0; i < 16; i++)
			gv = (gv << 4) + ga[i];

		var d = new Dictionary<long, int>();
		var q = new Queue<Tuple<long, P>>();
		d[gv] = 0;
		q.Enqueue(Tuple.Create(gv, gp));

		while (q.Any())
		{
			var t = q.Dequeue();
			if (d[t.Item1] == 45) continue;
			var id = ToId(t.Item2);
			foreach (var np in t.Item2.Nexts())
			{
				if (!np.IsInRange) continue;
				var nv = Swap(t.Item1, id, ToId(np));
				if (d.ContainsKey(nv)) continue;
				d[nv] = d[t.Item1] + 1;
				q.Enqueue(Tuple.Create(nv, np));
			}
		}

		Console.WriteLine(d[sv]);
	}

	static int ToId(P p) => 4 * p.i + p.j;
	static long GetValue(long v, int id) => (v >> 4 * (15 - id)) & 15;
	static long Swap(long v, int id1, int id2)
	{
		var v1 = GetValue(v, id1);
		var v2 = GetValue(v, id2);

		v -= (v1 - v2) << 4 * (15 - id1);
		v += (v1 - v2) << 4 * (15 - id2);
		return v;
	}

	struct P
	{
		const int h = 4, w = 4;
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j) };
	}
}
