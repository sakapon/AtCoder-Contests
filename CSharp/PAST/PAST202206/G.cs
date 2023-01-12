using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

		var uf = new UF(n + 1);

		foreach (var (a, b) in es)
		{
			if (!uf.Unite(a, b))
			{
				return false;
			}
		}
		return true;
	}
}
