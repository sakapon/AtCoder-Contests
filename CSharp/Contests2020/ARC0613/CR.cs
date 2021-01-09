using System;
using System.Linq;

class CR
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = Read();

		for (int c = 0; c < k; c++)
		{
			var raq = new StaticRAQ(n);
			for (int i = 0; i < n; i++)
				raq.Add(i - a[i], i + a[i] + 1, 1);
			var b = raq.GetAll();

			if (Enumerable.SequenceEqual(a, b)) break;
			a = b;
		}
		Console.WriteLine(string.Join(" ", a));
	}
}

class StaticRAQ
{
	int[] d;
	public StaticRAQ(int n) { d = new int[n]; }

	// O(1)
	// l_in < 0, r_ex >= n も可。
	public void Add(int l_in, int r_ex, int v)
	{
		d[Math.Max(0, l_in)] += v;
		if (r_ex < d.Length) d[r_ex] -= v;
	}

	// O(n)
	public int[] GetAll()
	{
		var a = new int[d.Length];
		a[0] = d[0];
		for (int i = 1; i < d.Length; ++i) a[i] = a[i - 1] + d[i];
		return a;
	}
}
