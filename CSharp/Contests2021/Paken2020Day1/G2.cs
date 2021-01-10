using System;
using System.Linq;

class G2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		var M = -1;

		for (int f = 0; f < 1 << n; f++)
		{
			var ok = true;
			foreach (var (l, r, x) in es)
				if (Enumerable.Range(l - 1, r - l + 1).Count(i => (f & (1 << i)) != 0) != x) { ok = false; break; }
			if (!ok) continue;
			M = Math.Max(M, Enumerable.Range(0, n).Count(i => (f & (1 << i)) != 0));
		}
		Console.WriteLine(M);
	}
}
