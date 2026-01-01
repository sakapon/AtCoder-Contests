class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();
		var c = Read();

		var s = 0L;
		var dp_b = new long[n];

		for (int i = 0; i < n; i++)
		{
			if (i != 0)
			{
				dp_b[i] = Math.Max(dp_b[i - 1], s) + b[i];
			}
			s += a[i];
		}

		var r = 0L;
		s = 0;
		for (int i = n - 1; i >= 2; i--)
		{
			s += c[i];
			r = Math.Max(r, dp_b[i - 1] + s);
		}
		return r;
	}
}
