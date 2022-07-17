using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class BL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Read();
		var a = Read();
		Read();
		var b = Read();

		var set = new BSArray<int>(a, true);
		return Array.TrueForAll(b, set.Contains) ? 1 : 0;
	}
}
