using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, m) = Read2();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var map = new bool[n, n];
		for (int i = 0; i < n; i++)
			for (int j = 0; j < n; j++)
				map[i, j] = IsNext(s[i], s[j]);

		var p = Enumerable.Range(0, n).ToArray();

		do
		{
			var ok = true;
			for (int k = 0; k < n - 1; k++)
			{
				if (!map[p[k], p[k + 1]])
				{
					ok = false;
					break;
				}
			}
			if (ok) return true;
		}
		while (NextPermutation(p));

		return false;

		bool IsNext(string s, string t)
		{
			var c = 0;
			for (int i = 0; i < m; i++)
				if (s[i] != t[i]) c++;
			return c == 1;
		}
	}

	public static bool NextPermutation(int[] p)
	{
		var n = p.Length;

		// p[i] < p[i + 1] を満たす最大の i
		var i = n - 2;
		while (i >= 0 && p[i] >= p[i + 1]) i--;
		if (i == -1) return false;

		// p[i] < p[j] を満たす最大の j
		var j = i + 1;
		while (j + 1 < n && p[i] < p[j + 1]) j++;

		(p[i], p[j]) = (p[j], p[i]);
		Array.Reverse(p, i + 1, n - i - 1);
		return true;
	}
}
