using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => Read());
		var rowSums = a.Select(v => v.Sum()).ToArray();

		var min = 1 << 30;
		var b = new bool[n, n];
		var rn = Enumerable.Range(0, n).ToArray();

		for (int x = 0; x < 1 << n + 2; x++)
		{
			var count = FlagCount(x);
			if (count + n < m || count > m) continue;

			Array.Clear(b, 0, n * n);
			for (int j = 0; j < n; j++)
			{
				if ((x & (1 << j)) == 0) continue;
				for (int i = 0; i < n; i++) b[i, j] = true;
			}
			if ((x & (1 << n)) != 0)
			{
				for (int i = 0; i < n; i++) b[i, i] = true;
			}
			if ((x & (1 << n + 1)) != 0)
			{
				for (int i = 0; i < n; i++) b[i, n - 1 - i] = true;
			}

			var rows = Array.ConvertAll(rn, i =>
			{
				var s = 0;
				for (int j = 0; j < n; j++)
					if (b[i, j]) s += a[i][j];
				return s;
			});
			var sum = rows.Sum();
			if (count == m) { min = Math.Min(min, sum); continue; }

			var diffs = Array.ConvertAll(rn, i => rowSums[i] - rows[i]);
			Array.Sort(diffs);
			min = Math.Min(min, sum + diffs.Take(m - count).Sum());
		}

		Console.WriteLine(min);
	}

	static int FlagCount(int x)
	{
		var r = 0;
		for (; x != 0; x >>= 1) if ((x & 1) != 0) ++r;
		return r;
	}
}
