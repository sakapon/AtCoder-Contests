using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var fts = GetFactorTypes(1000000);
		var u = new bool[1000000 + 1];

		if (IsPairwiseCoprime()) return "pairwise coprime";
		if (a.Aggregate(Gcd) == 1) return "setwise coprime";
		return "not coprime";

		bool IsPairwiseCoprime()
		{
			foreach (var x in a)
			{
				foreach (var f in fts[x])
				{
					if (u[f]) return false;
					u[f] = true;
				}
			}
			return true;
		}
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }

	static int[][] GetFactorTypes(int n)
	{
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int p = 2; p <= n; ++p)
			if (map[p].Count == 0)
				for (int x = p; x <= n; x += p)
					map[x].Add(p);
		return Array.ConvertAll(map, l => l.ToArray());
	}
}
