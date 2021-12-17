using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		s = s.Replace("axa", "...");
		s = s.Replace("ixi", "...");
		s = s.Replace("uxu", "...");
		s = s.Replace("exe", "...");
		s = s.Replace("oxo", "...");
		return s;
	}
}
