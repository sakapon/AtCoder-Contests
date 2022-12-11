using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		return Regex.IsMatch(s, "^[A-Z][1-9][0-9]{5}[A-Z]$");
	}
}
