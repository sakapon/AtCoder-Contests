using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var q =
			from i in Enumerable.Range(0, h - 1)
			from j in Enumerable.Range(0, w - 1)
			select new[] { s[i][j], s[i][j + 1], s[i + 1][j], s[i + 1][j + 1] }.Count(c => c == '#') % 2 == 1;
		Console.WriteLine(q.Count(b => b));
	}
}
