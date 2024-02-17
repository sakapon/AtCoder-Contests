class B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();
		var ps = Array.ConvertAll(new bool[n - 1], _ => Read2L());

		for (int i = 0; i < n - 1; i++)
		{
			var (s, t) = ps[i];
			a[i + 1] += a[i] / s * t;
		}
		return a[^1];
	}
}
