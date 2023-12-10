class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());
		var b = Array.ConvertAll(new bool[h], _ => Read());

		var r0 = SolveYoko();
		if (r0 == -1) return -1;

		a = Transpose(a);
		b = Transpose(b);

		var r1 = SolveYoko();
		if (r1 == -1) return -1;

		return r0 + r1;

		int SolveYoko()
		{
			var aset = a.Select(c => string.Join(" ", c.OrderBy(x => x))).ToList();
			var bset = b.Select(c => string.Join(" ", c.OrderBy(x => x))).ToArray();

			var r = 0;
			foreach (var s in bset)
			{
				var i = aset.IndexOf(s);
				if (i == -1) return -1;
				aset.RemoveAt(i);
				r += i;
			}
			return r;
		}
	}

	public static int[] GetColumn(int[][] m, int c) => Array.ConvertAll(m, r => r[c]);
	public static int[][] Transpose(int[][] m) => m[0].Select((x, c) => GetColumn(m, c)).ToArray();
}
