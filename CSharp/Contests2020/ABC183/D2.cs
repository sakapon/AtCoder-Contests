using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var raq = new StaticRAQ(200000);
		foreach (var p in ps)
			raq.Add(p[0], p[1], p[2]);
		Console.WriteLine(raq.GetAll().Max() <= w ? "Yes" : "No");
	}
}

class StaticRAQ
{
	long[] d;
	public StaticRAQ(int n) { d = new long[n]; }

	// O(1)
	// 範囲外のインデックスも可。
	public void Add(int l_in, int r_ex, long v)
	{
		d[Math.Max(0, l_in)] += v;
		if (r_ex < d.Length) d[r_ex] -= v;
	}

	// O(n)
	public long[] GetAll()
	{
		var a = new long[d.Length];
		a[0] = d[0];
		for (int i = 1; i < d.Length; ++i) a[i] = a[i - 1] + d[i];
		return a;
	}
}
