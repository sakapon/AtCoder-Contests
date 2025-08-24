using CoderLib8.DataTrees.SBTs;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();
		var b = Read();

		var st = new PushSBT<long>(n, Monoid.Int64_Add, a);

		foreach (var i in b)
		{
			var c = st[i];
			st[i] = -c;

			var q = Math.DivRem(c, n, out var rem);

			if (rem == 0)
			{
				st[0, n] = q;
			}
			else
			{
				// [i+1, i+1+rem) に q+1 ずつ。
				Add(i + 1, i + 1 + (int)rem, q + 1);
				Add(i + 1 + (int)rem, i + 1 + n, q);
			}
		}
		return string.Join(" ", Enumerable.Range(0, n).Select(i => st[i]));

		void Add(int l, int r, long x)
		{
			l %= n;
			r %= n;
			if (r == 0) r = n;

			if (l < r)
			{
				st[l, r] = x;
			}
			else
			{
				st[l, n] = x;
				st[0, r] = x;
			}
		}
	}
}
