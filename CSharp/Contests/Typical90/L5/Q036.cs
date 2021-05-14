using System;
using System.Linq;

class Q036
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());
		var qs = Array.ConvertAll(new bool[qc], _ => int.Parse(Console.ReadLine()));

		var xy1 = Array.ConvertAll(ps, p => p.x + p.y);
		var xy2 = Array.ConvertAll(ps, p => p.x - p.y);

		long max1 = xy1.Max();
		long min1 = xy1.Min();
		long max2 = xy2.Max();
		long min2 = xy2.Min();

		long Query(int i)
		{
			var d1 = Math.Max(max1 - xy1[i], xy1[i] - min1);
			var d2 = Math.Max(max2 - xy2[i], xy2[i] - min2);
			return Math.Max(d1, d2);
		}

		return string.Join("\n", qs.Select(q => Query(q - 1)));
	}
}
