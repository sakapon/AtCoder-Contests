using System;
using System.Linq;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Console.ReadLine();
		var s = Console.ReadLine().Split();
		var t = Console.ReadLine().Split().ToHashSet();
		return string.Join("\n", s.Select(t.Contains).Select(b => b ? "Yes" : "No"));
	}
}
