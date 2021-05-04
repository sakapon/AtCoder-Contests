using System;
using System.Linq;
using CoderLib8.Trees;

class Q017
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int l, int r) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var qs = Array.ConvertAll(new bool[m], _ => Read2());

		var st = new STR<int>(n + 1, (x, y) => x + y, 0);

		var c = 0L;
		foreach (var (l, r) in qs.OrderBy(_ => _.r).ThenBy(_ => -_.l))
		{
			c += st.Get(l) + st.Get(r);
			st.Set(l + 1, r, 1);
		}
		return c;
	}
}
