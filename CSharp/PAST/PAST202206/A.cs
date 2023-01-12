using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, a, b, c) = Read4();

		var ta = (decimal)x / a + c;
		var tb = (decimal)x / b;
		return ta < tb ? "Hare" : ta > tb ? "Tortoise" : "Tie";
	}
}
