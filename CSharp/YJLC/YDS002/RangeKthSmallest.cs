using System;

class RangeKthSmallest
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		int n = h[0], qc = h[1];
		var a = Array.ConvertAll(Read(), x => new[] { x });
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var st = new ST1<int[]>(n, (a1, a2) =>
		{
			var r = new int[a1.Length + a2.Length];
			Array.Copy(a1, r, a1.Length);
			Array.Copy(a2, 0, r, a1.Length, a2.Length);
			Array.Sort(r);
			return r;
		}, new int[0], a);

		foreach (var q in qs)
		{
			var r = Last(-1, 1 << 30, x => q[2] >= st.Aggregate(q[0], q[1], 0, (p, n, l) => p + First(0, st[n].Length, i => st[n][i] >= x)));
			Console.WriteLine(r);
		}
		Console.Out.Flush();
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
