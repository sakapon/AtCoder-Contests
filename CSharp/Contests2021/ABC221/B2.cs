using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var d = Enumerable.Range(0, s.Length).Where(i => s[i] != t[i]).ToArray();
		return s == t || d.Length == 2 && d[1] - d[0] == 1 && s[d[0]] == t[d[1]] && s[d[1]] == t[d[0]];
	}
}
