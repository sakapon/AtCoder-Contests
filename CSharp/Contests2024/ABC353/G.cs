using AlgorithmLib10.SegTrees.SegTrees111;

class G
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, c) = ((int, long))Read2L();
		var m = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[m], _ => ((int, long))Read2L());

		// 右から左に移動した場合
		// 左から右に移動した場合
		long OffsetR(int i) => c * i;
		long OffsetL(int i) => c * (n - 1 - i);

		var monoid = new Monoid<long>((x, y) => x >= y ? x : y, -1L << 60);
		var str = new MergeTree<long>(n, monoid);
		var stl = new MergeTree<long>(n, monoid);
		str[0] = -OffsetR(0);
		stl[0] = -OffsetL(0);

		foreach (var q in ps)
		{
			var (t, p) = q;
			t--;

			var vr = str[t, n] + OffsetR(t);
			var vl = stl[0, t] + OffsetL(t);
			var v = Math.Max(vr, vl) + p;

			str[t] = v - OffsetR(t);
			stl[t] = v - OffsetL(t);
		}

		var rn = Enumerable.Range(0, n).ToArray();
		var mr = rn.Max(i => str[i] + OffsetR(i));
		var ml = rn.Max(i => stl[i] + OffsetL(i));
		return Math.Max(mr, ml);
	}
}
