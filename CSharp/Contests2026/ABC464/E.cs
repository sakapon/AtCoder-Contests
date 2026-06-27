class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, qc) = Read3();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var s = NewArray2(h, w, 'A');
		var cursors = new int[h];

		foreach (var q in qs.Reverse())
		{
			var r = int.Parse(q[0]);
			var c = int.Parse(q[1]);
			var x = q[2][0];

			var si = First(0, h, i => cursors[i] < c);
			for (int i = si; i < r; i++)
				for (ref int j = ref cursors[i]; j < c; j++)
					s[i][j] = x;
		}

		return string.Join("\n", s.Select(cs => new string(cs)));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
