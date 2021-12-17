using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var b = Array.ConvertAll(new bool[n], _ => Read());

		var rn_ = Enumerable.Range(0, n - 1).ToArray();
		var rm = Enumerable.Range(0, m).ToArray();
		var rm_ = Enumerable.Range(0, m - 1).ToArray();

		if (!rm_.All(j => b[0][j + 1] - b[0][j] == 1)) return false;

		var s = "1234560";
		var bm = string.Join("", b[0].Select(x => x % 7));
		if (!s.Contains(bm)) return false;

		if (!rn_.All(i => rm.All(j => b[i + 1][j] - b[i][j] == 7))) return false;

		return true;
	}
}
