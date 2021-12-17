using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().OrderByDescending(c => c).ToArray();
		var m = s.Length;

		var r = 0L;

		AllBoolCombination(m, b =>
		{
			var s0 = "";
			var s1 = "";

			for (int i = 0; i < m; i++)
			{
				if (b[i])
				{
					s1 += s[i];
				}
				else
				{
					s0 += s[i];
				}
			}

			if (s0 == "" || s1 == "") return false;
			r = Math.Max(r, long.Parse(s0) * long.Parse(s1));

			return false;
		});

		return r;
	}

	public static void AllBoolCombination(int n, Func<bool[], bool> action)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		for (int x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
			if (action(b)) break;
		}
	}
}
