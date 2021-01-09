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
	long[] d;
	public StaticRAQ(int n) { d = new long[n]; }

	// O(1)
	// l_in < 0, r_ex >= n も可。
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
