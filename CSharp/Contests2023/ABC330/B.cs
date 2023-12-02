using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l, r) = Read3();
		var a = Read();

		for (int i = 0; i < n; i++)
		{
			a[i] = Math.Clamp(a[i], l, r);
		}
		return string.Join(" ", a);
	}
}
