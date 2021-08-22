using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var cs = CumSumL(a);
		Array.Sort(cs);

		return MInt(Enumerable.Range(0, n + 1).Sum(i => (2 * i - n) * (cs[i] % M)));
	}

	const long M = 998244353;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;

	public static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
