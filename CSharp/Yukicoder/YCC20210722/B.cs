using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		if (!IsPalindrome(s)) return n;
		if (n == 3) return -1;
		if (s.Distinct().Count() > 1) return n - 2;
		return -n % 2;
	}

	static bool IsPalindrome(string s)
	{
		for (int i = 0; i < s.Length; ++i) if (s[i] != s[s.Length - 1 - i]) return false;
		return true;
	}
}
