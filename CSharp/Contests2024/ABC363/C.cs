class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine().ToCharArray();

		if (s.Distinct().Count() == n) return Enumerable.Range(1, n).Aggregate((x, y) => x * y);

		var r = 0;
		Array.Sort(s);

		do
		{
			var ok = true;
			for (int i = 0; i + k <= n; i++)
			{
				if (IsPalindrome(s, i, i + k))
				{
					ok = false;
					break;
				}
			}
			if (ok) r++;
		}
		while (NextPermutation(s));

		return r;
	}

	public static bool NextPermutation(char[] p)
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

	static bool IsPalindrome(char[] s, int l, int r)
	{
		for (--l; ++l < --r;) if (s[l] != s[r]) return false;
		return true;
	}
}
