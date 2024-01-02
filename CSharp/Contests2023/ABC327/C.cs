using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = 9;
		var a = Array.ConvertAll(new bool[n], _ => Read());
		var rn = Enumerable.Range(0, n).ToArray();

		return
			rn.All(i => a[i].Distinct().Count() == n) &&
			rn.All(j => rn.Select(i => a[i][j]).Distinct().Count() == n) &&
			rn.Select(v => (i: v / 3 * 3, j: v % 3 * 3)).All(o => rn.Select(v => a[o.i + v / 3][o.j + v % 3]).Distinct().Count() == n);
	}
}
