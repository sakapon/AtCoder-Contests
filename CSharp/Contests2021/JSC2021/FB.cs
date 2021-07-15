using System;
using System.Collections.Generic;
using System.Linq;

class FB
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int t, int x, int y) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, qc) = Read3();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var map = new CompressionHashMap(qs.Select(q => q.y).Append(0).Append(-1).ToArray());

		var ab = new[] { new int[n + 1], new int[m + 1] };
		var counts = Array.ConvertAll(ab, _ => new BIT(map.Count));
		var sums = Array.ConvertAll(ab, _ => new BIT(map.Count));

		counts[0].Add(map[0], n);
		counts[1].Add(map[0], m);

		var r = 0L;

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var (t1, x, y) = q;
			t1--;
			var t2 = 1 - t1;

			var y0 = ab[t1][x];
			ab[t1][x] = y;
			counts[t1].Add(map[y0], -1);
			counts[t1].Add(map[y], 1);
			sums[t1].Add(map[y0], -y0);
			sums[t1].Add(map[y], y);

			r -= counts[t2].Sum(map[y0]) * y0;
			r -= sums[t2].Sum(map[y0] + 1, map.Count + 1);
			r += counts[t2].Sum(map[y]) * y;
			r += sums[t2].Sum(map[y] + 1, map.Count + 1);

			Console.WriteLine(r);
		}
		Console.Out.Flush();
	}
}

class BIT
{
	// Power of 2
	int n2 = 1;
	long[] a;

	public BIT(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 + 1];
	}

	public long this[int i]
	{
		get { return Sum(i) - Sum(i - 1); }
		set { Add(i, value - this[i]); }
	}

	public void Add(int i, long v)
	{
		for (; i <= n2; i += i & -i) a[i] += v;
	}

	public long Sum(int l_in, int r_ex) => Sum(r_ex - 1) - Sum(l_in - 1);
	public long Sum(int r_in)
	{
		var r = 0L;
		for (var i = r_in; i > 0; i -= i & -i) r += a[i];
		return r;
	}
}
