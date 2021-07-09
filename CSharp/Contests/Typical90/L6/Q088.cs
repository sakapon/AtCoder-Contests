using System;
using System.Linq;

class Q088
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, qc) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var set = new int[8889][];
		int[] b = null;
		int[] c = null;

		n = Math.Min(30, n);
		var rn = Enumerable.Range(0, n).ToArray();

		AllBoolCombination(n, f =>
		{
			foreach (var (x, y) in qs)
			{
				if (x > n || y > n) continue;
				if (f[x - 1] && f[y - 1]) return false;
			}

			var comb = Array.FindAll(rn, i => f[i]);
			var s = comb.Sum(i => a[i]);

			if (set[s] == null)
			{
				set[s] = comb;
				return false;
			}
			else
			{
				b = set[s];
				c = comb;
				return true;
			}
		});

		Console.WriteLine(b.Length);
		Console.WriteLine(string.Join(" ", b.Select(i => i + 1)));
		Console.WriteLine(c.Length);
		Console.WriteLine(string.Join(" ", c.Select(i => i + 1)));
	}

	public static void AllBoolCombination(int n, Func<bool[], bool> action)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		for (int x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
			if (action(b)) break;
		}
	}
}
