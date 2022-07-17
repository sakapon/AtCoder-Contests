using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		if (k == 1) return "Infinity";

		var lines = new int[n + 1];

		for (int i = 0; i < n; i++)
		{
			var (x1, y1) = ps[i];

			for (int j = i + 1; j < n; j++)
			{
				var (x2, y2) = ps[j];

				var c = 0;
				for (int z = 0; z < n; z++)
				{
					var (x, y) = ps[z];

					if ((x - x1) * (y2 - y1) == (x2 - x1) * (y - y1)) c++;
				}

				lines[c]++;
			}
		}

		for (int i = 2; i <= n; i++)
		{
			lines[i] /= i * (i - 1) / 2;
		}
		return lines[k..].Sum();
	}
}
