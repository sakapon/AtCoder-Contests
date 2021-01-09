using System;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var rn = Enumerable.Range(0, n).ToArray();
		var m = Enumerable.Range(0, 1 << n)
			.Select(x => Array.ConvertAll(rn, i => (x & (1 << i)) == 0 ? (a[i], false) : (b[i], true)))
			.Select(Swap)
			.Min();
		Console.WriteLine(m == 1 << 30 ? -1 : m);

		int Swap((int v, bool rev)[] c)
		{
			var g = Array.ConvertAll(c, _ => _.v);
			Array.Sort(g);
			var u = new bool[n];

			var map = new int[n];
			for (int i = 0; i < n; i++)
			{
				map[i] = -1;
				for (int j = 0; j < n; j++)
				{
					if (u[j] || g[i] != c[j].v || (i - j) % 2 != 0 != c[j].rev) continue;
					u[j] = true;
					map[i] = j;
					break;
				}
				if (map[i] == -1) return 1 << 30;
			}

			var t = 0;
			for (int i = 0; i < n; i++)
				for (int j = i + 1; j < n; j++)
					if (map[i] > map[j]) t++;
			return t;
		}
	}
}
