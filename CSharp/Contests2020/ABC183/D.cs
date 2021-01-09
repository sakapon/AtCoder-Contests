using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new bool[n], _ => ReadL());

		var query = ps.Select(p => (x: p[0], v: p[2]))
			.Concat(ps.Select(p => (x: p[1], v: -p[2])))
			.GroupBy(z => z.x)
			.OrderBy(g => g.Key)
			.Select(g => g.Sum(z => z.v));
		var s = CumSumL(query.ToArray());
		Console.WriteLine(s.Max() <= w ? "Yes" : "No");
	}

	public static long[] CumSumL(long[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
