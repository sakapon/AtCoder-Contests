using System;
using System.Linq;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, p, q, r) = Read4L();
		var a = ReadL();

		var s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
		var set = s.ToHashSet();

		for (int i = 0; i < n; i++)
		{
			var t = s[i];
			if (!set.Contains(t += p)) continue;
			if (!set.Contains(t += q)) continue;
			if (!set.Contains(t += r)) continue;
			return true;
		}
		return false;
	}
}
