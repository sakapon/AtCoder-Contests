class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var q = Read();
		var a = Read();
		var b = Read();

		var rn = Enumerable.Range(0, n).ToArray();
		var r = 0;

		for (int ac = 0; ac <= 1000000; ac++)
		{
			var bc = rn.Min(i => b[i] == 0 ? 1 << 30 : q[i] / b[i]);
			r = Math.Max(r, ac + bc);

			for (int i = 0; i < n; i++)
			{
				q[i] -= a[i];
				if (q[i] < 0) return r;
			}
		}
		return r;
	}
}
