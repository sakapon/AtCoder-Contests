using CoderLib8.DataTrees.SBTs;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var s = Console.ReadLine();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		if (n == 1)
		{
			var c = qs.Count(q => q.Item1 == 2);
			return string.Join("\n", Enumerable.Repeat("Yes", c));
		}

		var st = new MergeSBT<int>(n - 1, Monoid.Int32_Add, Enumerable.Range(0, n - 1).Select(i => s[i] == s[i + 1] ? 1 : 0));
		var bl = new List<bool>();

		foreach (var (t, l, r) in qs)
		{
			if (t == 1)
			{
				if (l > 1) st[l - 2] = 1 - st[l - 2];
				if (r < n) st[r - 1] = 1 - st[r - 1];
			}
			else
			{
				bl.Add(st[l - 1, r - 1] == 0);
			}
		}

		return string.Join("\n", bl.Select(b => b ? "Yes" : "No"));
	}
}
