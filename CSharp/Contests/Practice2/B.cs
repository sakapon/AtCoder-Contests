using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<long>();
		var h = Read();
		var n = h[0];
		var a = Read();

		var st = new ST(n);
		for (int i = 0; i < n; i++)
			st.Add(i, a[i]);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				st.Add(q[1], q[2]);
			else
				r.Add(st.Subsum(q[1], q[2]));
		}
		Console.WriteLine(string.Join("\n", r));
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

	public long Get(int i) => vs[kMax][i];
	public void Set(int i, long v) => Add(i, v - vs[kMax][i]);

	public void Add(int i, long v)
	{
		foreach (var (k, j) in GetLevels(i)) vs[k][j] += v;
	}

	public long Subsum(int minIn, int maxEx) => GetRange(minIn, maxEx).Sum(x => vs[x.k][x.i]);
}
