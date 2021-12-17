using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		Array.Sort(ps);
		var set = ps.ToHashSet();

		var r = 0;

		for (int i = 0; i < n; i++)
		{
			var (x1, y1) = ps[i];

			for (int j = i + 1; j < n; j++)
			{
				var (x2, y2) = ps[j];

				if (x1 < x2 && y1 < y2 && set.Contains((x1, y2)) && set.Contains((x2, y1)))
				{
					r++;
				}
			}
		}
		return r;
	}
}
