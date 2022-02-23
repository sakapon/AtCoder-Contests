using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (x1, y1, x2, y2) = Read4();

		var ds = new (int x, int y)[] { (1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1), (2, -1), (-2, 1), (-2, -1) };

		var set1 = ds.Select(v => (x1 + v.x, y1 + v.y)).ToHashSet();
		var set2 = ds.Select(v => (x2 + v.x, y2 + v.y)).ToHashSet();

		return set1.Overlaps(set2);
	}
}
