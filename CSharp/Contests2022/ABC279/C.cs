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

		var ts = Transpose(s);
		var tt = Transpose(t);
		Array.Sort(ts);
		Array.Sort(tt);
		return ts.SequenceEqual(tt);
	}

	public static string[] Transpose(string[] s)
	{
		var n = s.Length;
		var m = s[0].Length;
		var r = Array.ConvertAll(new bool[m], _ => new char[n]);
		for (int i = 0; i < n; ++i)
			for (int j = 0; j < m; ++j)
				r[j][i] = s[i][j];
		return Array.ConvertAll(r, l => new string(l));
	}
}
