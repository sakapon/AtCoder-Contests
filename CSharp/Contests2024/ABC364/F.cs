using CoderLib8.Collections.Dynamics.Int;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read3());

		var counts = new long[n];
		Array.Fill(counts, 1);

		var sum = 0L;
		// 区間を管理します。
		// 右端
		var set = new IntSegmentCountSet(n, counts);
		// 左端
		var ls = Enumerable.Range(0, n).ToArray();

		foreach (var q in qs.OrderBy(q => q.Item3))
		{
			var (l, r, c) = q;
			l--;
			r--;

			var r2 = set.GetFirstGeq(l);
			var nl = ls[r2];
			var nr = r2;
			set.Remove(r2);
			sum += c;

			while (true)
			{
				r2 = set.GetFirstGeq(l);
				if (r2 >= n) break;
				if (r < ls[r2]) break;
				nr = r2;
				set.Remove(r2);
				sum += c;
			}

			set.Add(nr);
			ls[nr] = nl;
		}

		if (set.Count == 1 && set.GetAt(0) == n - 1 && ls[^1] == 0) return sum;
		return -1;
	}
}
