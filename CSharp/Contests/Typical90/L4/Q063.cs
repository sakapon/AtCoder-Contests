using System;
using System.Linq;

class Q063
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var p = Array.ConvertAll(new bool[h], _ => Read());

		var d = new int[h * w + 1];
		var r = 0;

		var rh = Enumerable.Range(0, h).ToArray();
		AllCombination(rh, q =>
		{
			Array.Clear(d, 0, d.Length);

			for (int j = 0; j < w; j++)
			{
				// Distinct でも OK
				var vs = Array.ConvertAll(q, i => p[i][j]);
				if (!AreAllSame(vs)) continue;
				d[vs[0]]++;
			}
			r = Math.Max(r, q.Length * d.Max());

			return false;
		});

		return r;
	}

	static bool AreAllSame(int[] a)
	{
		if (a.Length == 0) return false;
		for (int i = 1; i < a.Length; ++i)
			if (a[i - 1] != a[i]) return false;
		return true;
	}

	public static void AllCombination<T>(T[] values, Func<T[], bool> action)
	{
		var n = values.Length;
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;

		var rn = new int[n];
		for (int i = 0; i < n; ++i) rn[i] = i;

		for (int x = 0; x < pn; ++x)
		{
			var indexes = Array.FindAll(rn, i => (x & (1 << i)) != 0);
			if (action(Array.ConvertAll(indexes, i => values[i]))) break;
		}
	}
}
