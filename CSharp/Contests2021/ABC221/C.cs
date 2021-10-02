using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().ToCharArray();
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

			var sc0 = s0.ToCharArray();
			Array.Sort(sc0);
			Array.Reverse(sc0);
			var x = long.Parse(new string(sc0));

			var sc1 = s1.ToCharArray();
			Array.Sort(sc1);
			Array.Reverse(sc1);
			var y = long.Parse(new string(sc1));

			r = Math.Max(r, x * y);

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
