using System;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read3());

		var map = NewArray2(n, n, -1);
		foreach (var (a, b, c) in es)
		{
			map[a - 1][b - 1] = c;
			map[b - 1][a - 1] = c;
		}

		var r = 0;

		Permutation(n, p =>
		{
			var s = 0;
			for (int i = 1; i < n; i++)
			{
				if (map[p[i - 1]][p[i]] == -1)
				{
					Chmax(ref r, s);
					return false;
				}
				s += map[p[i - 1]][p[i]];
			}
			Chmax(ref r, s);
			return false;
		});

		return r;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	public static int Chmax(ref int x, int v) => x < v ? x = v : x;

	// 値は何でもかまいません。重複可能。
	public static bool NextPermutation(int[] p)
	{
		var n = p.Length;

		// p[i] < p[i + 1] を満たす最大の i
		var i = n - 2;
		while (i >= 0 && p[i] >= p[i + 1]) --i;
		if (i < 0) return false;

		// p[i] < p[j] を満たす最大の j
		var j = i + 1;
		while (j + 1 < n && p[i] < p[j + 1]) ++j;

		(p[i], p[j]) = (p[j], p[i]);
		Array.Reverse(p, i + 1, n - i - 1);
		return true;
	}

	// [0, n) から n 個を選ぶ方法を列挙します。
	public static void Permutation(int n, Func<int[], bool> action)
	{
		var p = new int[n];
		for (int i = 0; i < n; ++i) p[i] = i;
		while (!action(p) && NextPermutation(p)) ;
	}
}
