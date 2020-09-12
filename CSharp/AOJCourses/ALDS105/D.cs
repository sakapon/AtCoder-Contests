using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).Select((x, i) => new { x, i }).OrderBy(_ => _.x).Select(_ => _.i).ToArray();

		var st = new ST(n);
		var r = 0L;
		for (int i = 0; i < n; i++)
		{
			r += st.Subsum(a[i], n);
			st.Add(a[i], 1);
		}
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
