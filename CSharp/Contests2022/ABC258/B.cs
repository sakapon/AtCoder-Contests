using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var rn = Enumerable.Range(0, n).ToArray();
		var l = new List<string>();

		l.AddRange(a);
		l.AddRange(rn.Select(j => string.Join("", rn.Select(i => a[i][j]))));
		l.AddRange(rn.Select(i => string.Join("", rn.Select(j => a[(i + j) % n][j]))));
		l.AddRange(rn.Select(i => string.Join("", rn.Select(j => a[(i - j + n) % n][j]))));

		var count = l.Count;
		for (int i = 0; i < count; i++)
		{
			var s = l[i];
			for (int j = 1; j < n; j++)
			{
				l.Add(s[j..] + s[..j]);
			}
		}

		count = l.Count;
		for (int i = 0; i < count; i++)
		{
			var s = l[i];
			l.Add(string.Join("", s.Reverse()));
		}

		l.Sort();
		return l[^1];
	}
}
