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
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var p = new int[n];
		Array.Fill(p, -1);
		for (int i = a.Length - 1; i >= 0; i--)
		{
			if (p[a[i] - 1] == -1)
			{
				p[a[i] - 1] = i;
			}
			else
			{
				p[a[i] - 1] -= i + 1;
			}
		}
		Array.Sort(p);

		var r = new int[2 * n - 1];
		foreach (var x in p)
		{
			r[x]++;
		}
		for (int i = 1; i < r.Length; i++)
		{
			r[i] += r[i - 1];
		}
		return string.Join("\n", r);
	}
}
