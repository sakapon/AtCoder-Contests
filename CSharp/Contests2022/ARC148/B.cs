using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var l = s.IndexOf('p');
		if (l == -1) return s;
		return Enumerable.Range(l, n - l + 1).Min(r => Convert(s, l, r));
	}

	static string Convert(string s, int l, int r)
	{
		var t = s[l..r];
		var m = t.Length;

		var ft = new char[m];
		for (int i = 0; i < m; i++)
		{
			ft[i] = t[m - 1 - i] == 'd' ? 'p' : 'd';
		}
		return s[..l] + new string(ft) + s[r..];
	}
}
