using System;
using System.Linq;

class Q076
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var sum = a.Sum();
		if (sum % 10 != 0) return false;
		sum /= 10;

		a = a.Concat(a).ToArray();
		var s = CumSumL(a);
		var set = s.ToHashSet();

		return s.Any(x => set.Contains(x + sum));
	}

	static long[] CumSumL(long[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
