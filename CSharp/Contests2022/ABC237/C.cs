using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();

		var start = s.Length - s.TrimStart('a').Length;
		var end = s.Length - s.TrimEnd('a').Length;

		if (start > end) return false;

		s = new string('a', end - start) + s;
		return IsPalindrome(s);
	}

	static bool IsPalindrome(string s)
	{
		for (int i = 0; i < s.Length; ++i) if (s[i] != s[s.Length - 1 - i]) return false;
		return true;
	}
}
