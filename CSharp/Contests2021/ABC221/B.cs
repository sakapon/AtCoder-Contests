using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine().ToCharArray();
		var t = Console.ReadLine().ToCharArray();

		if (s.SequenceEqual(t)) return true;

		for (int i = 1; i < s.Length; i++)
		{
			(s[i], s[i - 1]) = (s[i - 1], s[i]);
			if (s.SequenceEqual(t)) return true;
			(s[i], s[i - 1]) = (s[i - 1], s[i]);
		}
		return false;
	}
}
