using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Read();

		var sum = es.Sum();
		if (sum % 3 != 0) return false;
		sum /= 3;

		var rn = Enumerable.Range(0, n).ToArray();
		var ok = false;

		AllBoolCombination(n, b =>
		{
			var s = 0;
			for (int i = 0; i < n; i++)
			{
				if (b[i])
				{
					s += es[i];
				}
			}

			if (s != sum) return false;

			var inds = Array.FindAll(rn, i => !b[i]);
			var a = Array.ConvertAll(inds, i => es[i]);

			if (!Check2(a, sum)) return false;

			ok = true;
			return true;
		});

		return ok;
	}

	static bool Check2(int[] a, int sum)
	{
		var n = a.Length;

		var ok = false;

		AllBoolCombination(n, b =>
		{
			var s = 0;
			for (int i = 0; i < n; i++)
			{
				if (b[i])
				{
					s += a[i];
				}
			}
			if (s != sum) return false;

			ok = true;
			return true;
		});

		return ok;
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
