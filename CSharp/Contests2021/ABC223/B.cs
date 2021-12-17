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

		var r = new string[n];

		for (int i = 0; i < n; i++)
		{
			r[i] = s;
			s = s[1..] + s[0];
		}

		Array.Sort(r);
		return $"{r[0]}\n{r[^1]}";
	}
}
