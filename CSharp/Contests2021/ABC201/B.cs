using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Console.ReadLine().Split());
		return ps.OrderBy(p => -int.Parse(p[1])).ElementAt(1)[0];
	}
}
