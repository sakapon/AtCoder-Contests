using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Weak" : "Strong");
	static bool Solve()
	{
		var x = Console.ReadLine().Select(c => c - '0').ToArray();

		if (x.Distinct().Count() == 1) return true;
		if (Enumerable.Range(1, 3).All(i => x[i] == (x[i - 1] + 1) % 10)) return true;
		return false;
	}
}
