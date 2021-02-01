using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, m, x) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ =>
		{
			var v = Read();
			return (c: v[0], a: v.Skip(1).ToArray());
		});

		var min = 1 << 30;

		for (int f = 0; f < 1 << n; f++)
		{
			var cost = 0;
			var comp = new int[m];

			for (int i = 0; i < n; i++)
			{
				if ((f & (1 << i)) == 0) continue;
				cost += ps[i].c;
				for (int j = 0; j < m; j++)
				{
					comp[j] += ps[i].a[j];
				}
			}

			if (comp.All(v => v >= x))
			{
				min = Math.Min(min, cost);
			}
		}

		Console.WriteLine(min == 1 << 30 ? -1 : min);
	}
}
