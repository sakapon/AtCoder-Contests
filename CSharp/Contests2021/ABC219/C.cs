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
		var x = Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var d = new Dictionary<char, char>();
		for (int i = 0; i < x.Length; ++i) d[x[i]] = (char)('a' + i);

		var keys = Array.ConvertAll(s, v => string.Join("", v.Select(c => d[c])));
		Array.Sort(keys, s);

		return string.Join("\n", s);
	}
}
