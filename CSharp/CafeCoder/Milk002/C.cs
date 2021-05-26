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
		var s = Console.ReadLine();
		var x = long.Parse(Console.ReadLine());

		var d = Tally(s, '0', 10);

		var r = 0;
		var t = new int[10];
		for (long v = 0; v < 100000000000000; v += x)
		{
			Array.Clear(t, 0, 10);

			var ok = true;
			foreach (var c in v.ToString())
			{
				var i = c - '0';
				t[i]++;
				if (t[i] > d[i]) { ok = false; break; }
			}

			if (ok) r++;
		}
		return r;
	}

	static int[] Tally(string s, char start = 'A', int count = 26)
	{
		var r = new int[count];
		foreach (var c in s) ++r[c - start];
		return r;
	}
}
