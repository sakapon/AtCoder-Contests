using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], t = h[1];

		var raq = new StaticRAQ(t);
		for (int i = 0; i < n; i++)
		{
			var q = Read();
			raq.Add(q[0], q[1], 1);
		}
		Console.WriteLine(raq.GetAll().Max());
	}
}

class StaticRAQ
{
	int n;
	long[] d;
	public StaticRAQ(int _n) { n = _n; d = new long[n]; }

	// O(1)
	// 範囲外のインデックスも可。
	public void Add(int l_in, int r_ex, long v)
	{
		if (r_ex < 0 || n <= l_in) return;
		d[Math.Max(0, l_in)] += v;
		if (r_ex < n) d[r_ex] -= v;
	}

	// O(n)
	public long[] GetAll()
	{
		var a = new long[n];
		a[0] = d[0];
		for (int i = 1; i < n; ++i) a[i] = a[i - 1] + d[i];
		return a;
	}

	// O(n)
	// d をそのまま使います。
	public long[] GetAll0()
	{
		for (int i = 1; i < n; ++i) d[i] += d[i - 1];
		return d;
	}
}
