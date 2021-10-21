using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		var t = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var sps = s.ToPoints();
		var tps = t.ToPoints();

		for (int i = 0; i < 4; i++)
		{
			if (Equals()) return true;
			sps = sps.Rotate90();
			Array.Sort(sps);
		}
		return false;

		bool Equals()
		{
			if (sps.Length != tps.Length) return false;
			var d = (sps[0].i - tps[0].i, sps[0].j - tps[0].j);
			return sps.Zip(tps, (p, q) => (p.i - q.i, p.j - q.j)).All(p => p == d);
		}
	}
}
