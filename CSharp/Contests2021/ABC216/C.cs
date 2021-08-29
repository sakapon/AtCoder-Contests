using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var l = new List<char>();

		while (n > 0)
		{
			if (n % 2 == 0)
			{
				n /= 2;
				l.Add('B');
			}
			else
			{
				n--;
				l.Add('A');
			}
		}
		l.Reverse();

		return string.Join("", l);
	}
}
