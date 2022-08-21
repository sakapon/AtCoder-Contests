using System;
using System.Collections.Generic;
using System.Linq;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps1 = Array.ConvertAll(new bool[n], _ => Read2());
		var ps2 = Array.ConvertAll(new bool[n], _ => Read2());

		{
			Array.Sort(ps1);
			Array.Sort(ps2);
			if (ps1.SequenceEqual(ps2)) return true;
		}
		{
			var ps1x = Array.ConvertAll(ps1, p => (x: -p.x, p.y));
			Array.Sort(ps1x);
			var r = ps1x.Zip(ps2, (p, q) => (x: p.x - q.x, y: p.y - q.y)).Distinct().ToArray();
			if (r.Length == 1 && r[0].y == 0) return true;
		}
		{
			var ps1y = Array.ConvertAll(ps1, p => (p.x, y: -p.y));
			Array.Sort(ps1y);
			var r = ps1y.Zip(ps2, (p, q) => (x: p.x - q.x, y: p.y - q.y)).Distinct().ToArray();
			if (r.Length == 1 && r[0].x == 0) return true;
		}

		return false;
	}
}
