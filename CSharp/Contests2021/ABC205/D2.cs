using System;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var a = ReadL();
		var qs = Array.ConvertAll(new bool[qc], _ => long.Parse(Console.ReadLine()));

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var k in qs)
		{
			var r = First(0, 1L << 60, x =>
			{
				var c = First(0, n, i => a[i] > x);
				return x - c >= k;
			});
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

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
