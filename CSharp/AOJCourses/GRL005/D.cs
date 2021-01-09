using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		map = new int[n].Select(_ => Read().Skip(1).ToArray()).ToArray();
		var q = int.Parse(Console.ReadLine());
		var qs = new int[q].Select(_ => Read()).ToArray();

		tour = new List<int>();
		order = Array.ConvertAll(new int[n], _ => new List<int>());
		EulerTourDfs(0, 0);

		var st = new ST_Sum(2 * n);
		var r = new List<long>();
		foreach (var z in qs)
		{
			if (z[0] == 0)
			{
				st.Add(order[z[1]][0], order[z[1]].Last() + 1, z[2]);
			}
			else
			{
				r.Add(st.Get(order[z[1]][0]));
			}
		}
		Console.WriteLine(string.Join("\n", r));
	}

	static int[][] map;
	static List<int> tour;
	static List<int>[] order;
	static void EulerTourDfs(int v, int depth)
	{
		order[v].Add(tour.Count);
		foreach (var nv in map[v])
		{
			tour.Add(v);
			EulerTourDfs(nv, depth + 1);
			tour.Add(-nv);
			order[v].Add(tour.Count);
		}
	}
}

class ST_Sum
{
	struct KI
	{
		public int k, i;
	}

	int kMax;
	List<long[]> vs = new List<long[]> { new long[1] };
	public ST_Sum(int n)
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

	public void Add(int minIn, int maxEx, long v)
	{
		foreach (var x in GetRange(minIn, maxEx)) vs[x.k][x.i] += v;
	}

	public long Get(int i) => GetLevels(i).Sum(x => vs[x.k][x.i]);
}
