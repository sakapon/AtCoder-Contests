using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class D
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var uf = new UF(1 << 7);
		foreach (var e in es)
		{
			uf.Unite(e[0][0], e[1][0]);
		}
		return s.Select(c => uf.GetRoot(c)).SequenceEqual(t.Select(c => uf.GetRoot(c)));
	}
}
