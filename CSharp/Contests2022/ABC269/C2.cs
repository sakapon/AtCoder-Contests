using System;
using System.Linq;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = ulong.Parse(Console.ReadLine());

		var fs = new bool[60].Select((_, i) => 1UL << i).Where(f => (n & f) != 0).ToArray();
		return string.Join("\n", CreateAllSums(fs));
	}

	public static ulong[] CreateAllSums(ulong[] a)
	{
		var n = a.Length;
		var r = new ulong[1 << n];
		for (int i = 0, pi = 1; i < n; ++i, pi <<= 1)
			for (int x = 0; x < pi; ++x)
				r[x | pi] = r[x] | a[i];
		return r;
	}
}
