using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());

		var all = ps.SelectMany(p => p.Distinct()).ToArray();
		return ps.All(p => all.Count(q => q == p[0]) == 1 || all.Count(q => q == p[1]) == 1);
	}
}
