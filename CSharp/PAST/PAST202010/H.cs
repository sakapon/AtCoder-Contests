using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1], k = h[2];
		var s = Array.ConvertAll(new int[n], _ => Console.ReadLine().ToCharArray());

		var l = new List<char>();
		for (int q = 1; q <= Math.Min(n, m); q++)
		{
			var ok = false;

			for (int si = 0; si < n - q + 1; si++)
			{
				if (ok) break;
				for (int sj = 0; sj < m - q + 1; sj++)
				{
					l.Clear();
					for (int i = 0; i < q; i++)
						for (int j = 0; j < q; j++)
							l.Add(s[si + i][sj + j]);

					var count = l.GroupBy(c => c).Select(g => g.Count()).OrderBy(x => -x).First();
					if (count + k >= q * q) { ok = true; break; }
				}
			}

			if (ok) continue;
			Console.WriteLine(q - 1);
			return;
		}
		Console.WriteLine(Math.Min(n, m));
	}
}
