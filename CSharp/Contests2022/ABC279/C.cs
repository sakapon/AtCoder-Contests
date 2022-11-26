using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());
		var t = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var ts = Trans(s);
		var tt = Trans(t);
		return ts.SequenceEqual(tt);
	}

	static string[] Trans(string[] s)
	{
		var rh = Enumerable.Range(0, s.Length).ToArray();
		var rw = Enumerable.Range(0, s[0].Length).ToArray();
		s = rw.Select(j => string.Join("", rh.Select(i => s[i][j]))).ToArray();
		Array.Sort(s);
		return s;
	}
}
