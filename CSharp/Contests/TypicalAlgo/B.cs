using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var r = 0;
		var t = 0;

		foreach (var (a, b) in ps.OrderBy(p => p.Item2))
		{
			if (t < a)
			{
				r++;
				t = b;
			}
		}
		return r;
	}
}
