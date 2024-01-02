using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var z = Console.ReadLine().Split();
		var n = int.Parse(z[0]);
		var t = z[1];

		var r = new List<int>();

		for (int k = 1; k <= n; k++)
		{
			var s = Console.ReadLine();
			if (AlmostEquals(s, t)) r.Add(k);
		}
		return $"{r.Count}\n" + string.Join(" ", r);
	}

	static bool AlmostEquals(string s, string t)
	{
		if (s.Length == t.Length)
		{
			if (s == t) return true;
			return s.Zip(t).Count(p => p.First != p.Second) == 1;
		}
		else if (s.Length + 1 == t.Length)
		{
			return IsAdding(s, t);
		}
		else if (s.Length == t.Length + 1)
		{
			return IsAdding(t, s);
		}
		return false;
	}

	static bool IsAdding(string s1, string s2)
	{
		if (s2.StartsWith(s1)) return true;
		var di = Enumerable.Range(0, s1.Length).First(i => s1[i] != s2[i]);
		return s1[di..] == s2[(di + 1)..];
	}
}
