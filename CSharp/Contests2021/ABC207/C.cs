using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var ps2 = ps.Select(p =>
		{
			var (t, l, r) = p;
			l *= 2;
			r *= 2;
			if (t >= 3) l++;
			if (t % 2 == 1) r++;
			return (l, r);
		}).OrderBy(p => p.l).ToArray();

		var r = 0;
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				if (ps2[j].l < ps2[i].r) r++;
		return r;
	}
}
