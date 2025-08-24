class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var dp0 = 0L;
		var dp1 = -1L << 60;

		for (int i = 0; i < n; i++)
		{
			(dp0, dp1) = (Math.Max(dp0, dp1 + 2 * a[i]), Math.Max(dp0 + a[i], dp1));
		}
		return Math.Max(dp0, dp1);
	}
}
