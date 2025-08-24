using System;
using System.Collections.Generic;

class Q027B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		const int f = (1 << 18) - 1;
		var table = Array.ConvertAll(new bool[f + 1], _ => new List<string>());
		var r = new List<int>();

		for (int i = 1; i <= n; i++)
		{
			var s = Console.ReadLine();
			var l = table[Hash(s) & f];
			if (l.Contains(s)) continue;
			l.Add(s);
			r.Add(i);
		}
		return string.Join("\n", r);
	}

	static int Hash(string s)
	{
		var h = 0;
		foreach (var c in s) h = h * 987654323 + c;
		return h;
	}
}
