using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = Read();

		var q = new List<(int i, int d)>();
		for (int c = 0; c < k; c++)
		{
			var t = 0;

			for (int i = 0; i < n; i++)
			{
				if (i > a[i]) q.Add((i - a[i], 1));
				else t++;

				if (i + a[i] + 1 < n) q.Add((i + a[i] + 1, -1));
			}
			q.Sort();

			var b = new int[n];
			for (int qi = 0, i = 0; i < n; i++)
			{
				while (qi < q.Count && q[qi].i == i)
					t += q[qi++].d;
				b[i] = t;
			}

			if (Enumerable.SequenceEqual(a, b)) break;
			a = b;
			q.Clear();
		}
		Console.WriteLine(string.Join(" ", a));
	}
}
