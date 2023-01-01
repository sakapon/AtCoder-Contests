using System;
using System.Collections.Generic;
using System.Linq;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Read());
		var n = int.Parse(Console.ReadLine());

		while (n-- > 0)
		{
			var (r, c) = Read2();
			r--; c--;

			while (r > 0 && s[r - 1][c] != 0)
			{
				s[r][c] = s[r - 1][c];
				r--;
			}
			s[r][c] = 0;
		}

		foreach (var r in s)
		{
			Console.WriteLine(string.Join(" ", r));
		}
	}
}
