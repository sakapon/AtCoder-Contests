using System;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var ps0 = ps.Where(p => p.Item1 == 0).Select(p => p.Item2).ToArray();
		var ps1 = ps.Where(p => p.Item1 == 1).Select(p => p.Item2).ToArray();
		var ps2 = ps.Where(p => p.Item1 == 2).Select(p => p.Item2).ToArray();

		Array.Sort(ps0);
		Array.Sort(ps1);
		Array.Sort(ps2);
		Array.Reverse(ps0);
		Array.Reverse(ps1);
		Array.Reverse(ps2);

		ps0 = ps0.Concat(new int[m]).ToArray();
		var i0 = m;
		var i1 = 0;

		var r = ps0[0..m].Sum(x => (long)x);

		for (int i2 = 0; i2 < ps2.Length; i2++)
		{
			var t = r;

			if (i0 == 0) break;
			t -= ps0[--i0];

			for (int k = 0; k < ps2[i2]; k++)
			{
				if (i0 == 0) break;
				if (i1 == ps1.Length) break;
				if (ps0[i0 - 1] > ps1[i1]) break;
				t -= ps0[--i0];
				t += ps1[i1++];
			}

			r = Math.Max(r, t);
			if (i1 == ps1.Length) break;
		}

		return r;
	}
}
