using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var n = s.Length;
		var m = t.Length;
		var rm = Enumerable.Range(0, m).ToArray();

		return Enumerable.Range(0, n - m + 1).Min(i => Diff(s.Substring(i, m), t));

		int Diff(string s, string t)
		{
			return rm.Count(i => s[i] != t[i]);
		}
	}
}
