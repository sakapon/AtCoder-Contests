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
		var (n, k) = Read2();
		var a = Read();

		var b = new int[n];
		var map = Enumerable.Range(0, n).ToLookup(i => i % k, i => a[i]);

		for (int j = 0; j < k; j++)
		{
			var p = map[j].ToArray();
			Array.Sort(p);

			for (int i = 0; i < p.Length; i++)
			{
				b[k * i + j] = p[i];
			}
		}

		Array.Sort(a);
		return a.SequenceEqual(b);
	}
}
