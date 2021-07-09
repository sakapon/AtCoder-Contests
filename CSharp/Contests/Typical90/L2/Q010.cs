using System;

class Q010
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int c, int p) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var rsq1 = new StaticRSQ1(Array.ConvertAll(ps, p => p.c == 1 ? p.p : 0L));
		var rsq2 = new StaticRSQ1(Array.ConvertAll(ps, p => p.c == 2 ? p.p : 0L));

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var (l, r) in qs)
		{
			Console.WriteLine($"{rsq1.GetSum(l - 1, r)} {rsq2.GetSum(l - 1, r)}");
		}
		Console.Out.Flush();
	}
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public StaticRSQ1(long[] a)
	{
		n = a.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public long GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}
