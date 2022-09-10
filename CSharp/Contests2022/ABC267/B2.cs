using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();

		if (s[0] == 1 || !s.Contains(1)) return false;
		var c = new int[7];
		for (int i = 0; i < 10; i++) c["3241350246"[i] - '0'] |= s[i];

		var l = Array.IndexOf(c, 1);
		var r = Array.LastIndexOf(c, 1);
		return c[l..r].Contains(0);
	}
}
