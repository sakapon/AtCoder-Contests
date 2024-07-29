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
		var set1 = new IntSegmentCountSet(n, counts);
		var set2 = new IntSegmentCountSet(n);
		var rs = new int[n];
		Array.Fill(rs, -1);

		foreach (var q in qs.OrderBy(q => q.Item3))
		{
			var (l, r, c) = q;
			l--;
			r--;

			while (true)
			{
				var i = set1.GetFirstGeq(l);
				if (i > r) break;
				set1.Remove(i);
				sum += c;
			}

			var l2 = set2.GetLastLeq(l - 1);
			var r2 = l2 != -1 ? rs[l2] : -1;
			if (r2 < l) l2 = l;
			if (l2 < l)
			{
				set2.Remove(l2);
				sum += c;
			}

			while (true)
			{
				var i = set2.GetFirstGeq(l2);
				if (i > r) break;
				set2.Remove(i);
				sum += c;
				r2 = rs[i];
			}
			if (r2 < r) r2 = r;

			set2.Add(l2);
			rs[l2] = r2;
		}

		if (set2.Count == 1 && set2.GetAt(0) == 0 && rs[0] == n - 1) return sum;
		return -1;
	}
}
