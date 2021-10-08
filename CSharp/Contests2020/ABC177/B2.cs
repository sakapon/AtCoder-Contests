using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var n = s.Length;
		var m = t.Length;

		return Enumerable.Range(0, n - m + 1).Min(i => Diff(s, t, i));

		int Diff(string s, string t, int i)
		{
			var r = 0;
			for (int j = 0; j < m; j++)
			{
				if (s[i + j] != t[j])
				{
					r++;
				}
			}
			return r;
		}
	}
}
