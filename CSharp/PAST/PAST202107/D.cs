using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().ToCharArray();

		var set = "aiueo".ToHashSet();

		for (int i = 1; i < n - 1; i++)
		{
			if (s[i] != 'x') continue;
			if (s[i - 1] != s[i + 1]) continue;
			if (!set.Contains(s[i - 1])) continue;

			s[i - 1] = s[i] = s[i + 1] = '.';
		}

		return new string(s);
	}
}
