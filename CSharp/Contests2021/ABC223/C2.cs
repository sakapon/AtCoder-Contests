using System;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var ts = Array.ConvertAll(ps, p => (double)p.a / p.b);
		var t2 = ts.Sum() / 2;

		var r = 0.0;
		for (int i = 0; i < n; i++)
		{
			var t = ts[i];

			if (t2 <= t)
			{
				r += ps[i].b * t2;
				return r;
			}

			r += ps[i].a;
			t2 -= t;
		}
		return -1;
	}
}
