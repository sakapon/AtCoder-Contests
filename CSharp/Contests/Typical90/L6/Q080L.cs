using System;
using System.Linq;
using System.Numerics;

class Q080L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static ulong[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), ulong.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();
		var a = ReadL();

		var rn = Enumerable.Range(0, n).ToArray();
		return InclusionExclusion(n, GetCount);

		long GetCount(bool[] b)
		{
			var or = rn.Select(i => b[i] ? a[i] : 0).Aggregate((x, y) => x | y);
			return 1L << d - BitOperations.PopCount(or);
		}
	}

	public static long InclusionExclusion(int n, Func<bool[], long> getCount)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		var r = 0L;
		for (uint x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;

			var sign = BitOperations.PopCount(x) % 2 == 0 ? 1 : -1;
			r += sign * getCount(b);
		}
		return r;
	}
}
