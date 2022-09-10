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
		var s = Console.ReadLine().Select(c => c - '0').ToArray();

		if (s[0] == 1) return false;

		var c = new int[7];
		c[0] = s[6];
		c[1] = s[3];
		c[2] = s[1] | s[7];
		c[3] = s[4];
		c[4] = s[2] | s[8];
		c[5] = s[5];
		c[6] = s[9];

		var t = string.Join("", c);
		while (true)
		{
			var u = t.Replace("00", "0");
			if (t == u) break;
			t = u;
		}
		return t.Contains("101");
	}
}
