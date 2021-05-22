using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		return s[3] == '-' && s.Remove(3, 1).All(c => c != '-');
	}
}
