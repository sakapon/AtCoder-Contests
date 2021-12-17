using System;

class DB
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var l = Read();

		Array.Sort(l);

		var r = 0;
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				r += Last(-1, n - 1, x => l[x] < l[i] + l[j]) - j;
		Console.WriteLine(r);
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
