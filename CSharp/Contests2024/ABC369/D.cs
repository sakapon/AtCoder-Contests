class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var dp0 = new long[n + 1];
		var dp1 = new long[n + 1];
		dp1[0] = -1L << 60;

		for (int i = 0; i < n; i++)
		{
			dp0[i + 1] = Math.Max(dp0[i], dp1[i] + 2 * a[i]);
			dp1[i + 1] = Math.Max(dp0[i] + a[i], dp1[i]);
		}
		return Math.Max(dp0[n], dp1[n]);
	}
}
