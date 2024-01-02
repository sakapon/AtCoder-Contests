class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read();

		Array.Sort(a);
		var rk = Enumerable.Range(0, k / 2).ToArray();

		if (k % 2 == 0)
		{
			return rk.Sum(i => a[2 * i + 1] - a[2 * i]);
		}
		else
		{
			var t = rk.Sum(i => a[2 * i + 2] - a[2 * i + 1]);
			var r = t;

			for (int i = 1; i < k; i++)
			{
				if (i % 2 == 1) continue;

				t += a[i - 1] - a[i - 2];
				t -= a[i] - a[i - 1];
				r = Math.Min(r, t);
			}
			return r;
		}
	}
}
