class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var b = a.Select(x => -x).ToArray();
		Array.Reverse(b);
		return Math.Max(Solve(a), Solve(b));

		int Solve(int[] a)
		{
			var (r, p) = Lis(a);
			var c = Array.IndexOf(r, n);

			var l = new List<int>();
			for (int i = r[c - 1]; i != -1; i = p[i])
				l.Add(i);
			l.Reverse();

			if (l[0] > 0) return c + 1;
			if (l[c - 1] < n - 1) return c + 1;

			for (int i = 1; i < c; i++)
				if (l[i] - l[i - 1] > 1 && a[l[i]] - a[l[i - 1]] > 1) return c + 1;
			return c;
		}
	}

	// 改造
	public static (int[], int[]) Lis(int[] a)
	{
		var n = a.Length;
		var r = Array.ConvertAll(new bool[n + 1], _ => n);
		var p = new int[n];
		for (int i = 0; i < n; ++i)
		{
			var j = Min(0, n, x => r[x] == n || a[r[x]] >= a[i]);
			r[j] = i;
			p[i] = j > 0 ? r[j - 1] : -1;
		}
		return (r, p);
	}

	static int Min(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
