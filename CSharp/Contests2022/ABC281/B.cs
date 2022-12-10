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
		var s = Console.ReadLine();

		if (s.Length != 8) return false;
		if (!char.IsLetter(s[0])) return false;
		if (!char.IsLetter(s[^1])) return false;

		try
		{
			var i = int.Parse(s[1..^1]);
			return s[1] >= '1';
		}
		catch (Exception)
		{
			return false;
		}
	}
}
