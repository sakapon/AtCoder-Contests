using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var dy = ps.Select(v => v[1]).Concat(ps.Select(v => v[3])).Distinct().OrderBy(y => y).Select((y, i) => new { y, i }).ToDictionary(_ => _.y, _ => _.i);

		var xs = ps.Where(v => v[1] == v[3]).SelectMany(v => new[] { new[] { -1, Math.Min(v[0], v[2]), dy[v[1]] }, new[] { 1, Math.Max(v[0], v[2]), dy[v[1]] } }).ToArray();
		var ys = ps.Where(v => v[0] == v[2]).Select(v => new[] { 0, v[0], dy[Math.Min(v[1], v[3])], dy[Math.Max(v[1], v[3])] }).ToArray();
		var qs = xs.Concat(ys).OrderBy(q => q[1]).ThenBy(q => q[0]);

		var r = 0L;
		var st = new ST(dy.Count);
		foreach (var q in qs)
			if (q[0] == 0)
				r += st.Subsum(q[2], q[3] + 1);
			else
				st.Add(q[2], -q[0]);
		Console.WriteLine(r);
	}
}

class ST
{
	struct KI
	{
		public int k, i;
	}

	int kMax;
	List<long[]> vs = new List<long[]> { new long[1] };
	public ST(int n)
	{
		for (int c = 1; c < n; vs.Add(new long[c <<= 1])) ;
		kMax = vs.Count - 1;
	}

	KI[] GetLevels(int i)
	{
		var r = new List<KI>();
		for (int k = kMax; k >= 0; --k, i >>= 1) r.Add(new KI { k = k, i = i });
		return r.ToArray();
	}

	KI[] GetRange(int minIn, int maxEx)
	{
		var r = new List<KI>();
		for (int k = kMax, f = 1; k >= 0 && minIn < maxEx; --k, f <<= 1)
		{
			if ((minIn & f) != 0) r.Add(new KI { k = k, i = (minIn += f) / f - 1 });
			if ((maxEx & f) != 0) r.Add(new KI { k = k, i = (maxEx -= f) / f });
		}
		return r.ToArray();
	}

	public void Add(int i, long v)
	{
		foreach (var x in GetLevels(i)) vs[x.k][x.i] += v;
	}

	public long Subsum(int minIn, int maxEx) => GetRange(minIn, maxEx).Sum(x => vs[x.k][x.i]);
}
