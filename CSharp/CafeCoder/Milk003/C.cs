using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var ps = Array.ConvertAll(new bool[52], _ => Console.ReadLine());
		return ps.Distinct().Count() == 52;
	}
}
