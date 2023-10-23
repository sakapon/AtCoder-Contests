using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p, q) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var rs = Enumerable.Range(1, n).ToArray();
		var ids = (int[])rs.Clone();
		var ss = Array.ConvertAll(ps, t => p * t.a + q * t.b);

		Array.Sort(ss, ids);
		Array.Reverse(ss);
		Array.Reverse(ids);

		for (int i = 1; i < n; i++)
		{
			if (ss[i] == ss[i - 1])
			{
				rs[i] = rs[i - 1];
			}
		}

		Array.Sort(ids, rs);
		return string.Join("\n", rs);
	}
}
