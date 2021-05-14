using System;
using CoderLib8.Trees;

class Q037
{
	const long min = -1L << 60;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (w, n) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var st = new ST1<long>(w + 1, Math.Max, min);
		st.Set(0, 0);

		foreach (var (l, r, v) in ps)
		{
			for (int x = w; x > 0; x--)
			{
				var m = st.Get(Math.Max(0, x - r), Math.Max(0, x - l + 1));
				if (m == min) continue;
				st.Set(x, Math.Max(st.Get(x), m + v));
			}
		}
		var mv = st.Get(w);
		return mv == min ? -1 : mv;
	}
}
