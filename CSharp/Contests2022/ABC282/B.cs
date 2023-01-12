using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var f = (1 << m) - 1;
		var b = ss.Select(s => Enumerable.Range(0, m).Sum(i => s[i] == 'o' ? 1 << i : 0)).ToArray();

		var r = 0;
		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				if ((b[i] | b[j]) == f) r++;
			}
		}
		return r;
	}
}
