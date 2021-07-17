using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, qc) = Read2();
		var a = ReadL();
		var qs = Array.ConvertAll(new bool[qc], _ => long.Parse(Console.ReadLine()));

		var b = a.Select((v, i) => v - i - 1).ToArray();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var k in qs)
		{
			if (k > b[^1])
			{
				Console.WriteLine(a[^1] + k - b[^1]);
			}
			else
			{
				var i = First(0, n, x => b[x] >= k);
				Console.WriteLine(a[i] + k - b[i] - 1);
			}
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
