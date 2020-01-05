using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var rs = new int[n - 1].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		var map = rs.Concat(rs.Select(r => new[] { r[1], r[0] })).ToLookup(r => r[0], r => r[1]);

		var tour = new int[n + 1].Select(_ => new List<int>()).ToArray();
		var c = 0;
		var q = new Stack<int>();
		var parents = new Stack<int>();
		var u = new bool[n + 1];

		q.Push(1);
		parents.Push(0);
		while (q.Any())
		{
			var p = q.Peek();
			if (!u[p])
			{
				u[p] = true;
				var p0 = parents.Peek();
				if (p != 1) tour[p0].Add(++c);
				foreach (var x in map[p])
				{
					if (x == p0) continue;
					q.Push(x);
				}
				parents.Push(p);
			}
			else
			{
				if (p != 1) tour[p].Add(++c);
				q.Pop();
				parents.Pop();
			}
		}

		Console.WriteLine((tour.Select(l => Hole(l, rs.Length)).Aggregate((x, y) => x + y) / 2).V);
	}

	static MInt Hole(List<int> l, int m)
	{
		if (l.Count < 2) return 0;
		var whites = Enumerable.Range(0, l.Count).Select(i => ((l[(i + 1) % l.Count] - l[i]) / 2 + m) % m).Select(x => 1 / ((MInt)2).Pow(x)).ToArray();
		var allWhite = whites.Aggregate((x, y) => x * y);
		return 1 - allWhite * (1 + whites.Select(x => 1 / x - 1).Aggregate((x, y) => x + y));
	}
}

struct MInt
{
	const int M = 1000000007;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }

	public static implicit operator MInt(long v) => new MInt(v);
	public static MInt operator +(MInt x) => x;
	public static MInt operator -(MInt x) => -x.V;
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x * y.Inv();

	public MInt Pow(int i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);

	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
}
