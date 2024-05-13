using AlgorithmLib10.SegTrees.SegTrees111;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, c) = ((int, long))Read2();
		var m = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[m], _ => ((int, long))Read2L());

		// 右から左に移動した場合
		// 左から右に移動した場合
		var ar = new long[n];
		var al = new long[n];
		Array.Fill(ar, -1L << 60);
		Array.Fill(al, -1L << 60);
		ar[0] = -OffsetR(0);
		al[0] = -OffsetL(0);

		long OffsetR(int i) => c * i;
		long OffsetL(int i) => c * (n - 1 - i);

		var str = new MergeTree<int>(n, Int32_ArgMax(ar));
		var stl = new MergeTree<int>(n, Int32_ArgMax(al));
		for (int i = 0; i < n; i++)
		{
			str[i] = i;
			stl[i] = i;
		}

		foreach (var q in ps)
		{
			var (t, p) = q;
			t--;

			var ir = str[t, n];
			var il = stl[0, t];

			var v = 0L;
			if (ir == -1)
			{
				v = al[il] + OffsetL(t);
			}
			else if (il == -1)
			{
				v = ar[ir] + OffsetR(t);
			}
			else
			{
				v = Math.Max(ar[ir] + OffsetR(t), al[il] + OffsetL(t));
			}
			v += p;

			ar[t] = v - OffsetR(t);
			al[t] = v - OffsetL(t);
			str[t] = t;
			stl[t] = t;
		}

		var rn = Enumerable.Range(0, n).ToArray();
		var mr = rn.Max(i => ar[i] + OffsetR(i));
		var ml = rn.Max(i => al[i] + OffsetL(i));
		return Math.Max(mr, ml);
	}

	public static Monoid<int> Int32_ArgMax(long[] a) => new Monoid<int>((x, y) => y == -1 ? x : x == -1 ? y : a[x] >= a[y] ? x : y, -1);
}
