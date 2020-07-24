using System;
using System.Collections.Generic;
using System.Linq;

class N
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], q = h[1];
		var rs = new int[n].Select(_ => Read()).ToArray();
		var ps = new int[q].Select(_ => Read()).ToArray();

		var dy = rs.SelectMany(v => new[] { v[1], v[1] + v[2] })
			.Concat(ps.Select(v => v[1]))
			.Distinct()
			.OrderBy(y => y)
			.Select((y, i) => (y, i))
			.ToDictionary(v => v.y, v => v.i);

		var c = new long[q];
		var st = new ST(dy.Count);
		var qs = rs
			.Select(v => (x: v[0], X: v[0] + v[2], y: dy[v[1]], Y: dy[v[1] + v[2]], c: v[3]))
			.SelectMany(v => new[] { (q: -1, v.x, v.y, v.Y, v.c), (q: 1, x: v.X, v.y, v.Y, c: -v.c) })
			.Concat(ps.Select((v, id) => (q: 0, x: v[0], y: dy[v[1]], Y: 0, c: id)))
			.OrderBy(v => v.x)
			.ThenBy(v => v.q);

		foreach (var v in qs)
			if (v.q == 0)
				c[v.c] = st.Get(v.y);
			else
				st.Add(v.y, v.Y + 1, v.c);
		Console.WriteLine(string.Join("\n", c));
	}
}

class ST
{
	int kMax;
	List<long[]> vs = new List<long[]> { new long[1] };
	public ST(int n)
	{
		for (int c = 1; c < n; vs.Add(new long[c <<= 1])) ;
		kMax = vs.Count - 1;
	}

	(int k, int i)[] GetLevels(int i)
	{
		var r = new List<(int, int)>();
		for (int k = kMax; k >= 0; --k, i >>= 1) r.Add((k, i));
		return r.ToArray();
	}

	(int k, int i)[] GetRange(int minIn, int maxEx)
	{
		var r = new List<(int, int)>();
		for (int k = kMax, f = 1; k >= 0 && minIn < maxEx; --k, f <<= 1)
		{
			if ((minIn & f) != 0) r.Add((k, (minIn += f) / f - 1));
			if ((maxEx & f) != 0) r.Add((k, (maxEx -= f) / f));
		}
		return r.ToArray();
	}

	public void Add(int minIn, int maxEx, long v)
	{
		foreach (var (k, i) in GetRange(minIn, maxEx))
			vs[k][i] += v;
	}

	public long Get(int i) => GetLevels(i).Sum(x => vs[x.k][x.i]);
}
