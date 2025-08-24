class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		return Enumerable.Range(1, 1000000).Select(x => (long)x * x * x).Where(IsPali).Last(x => x <= n);
	}

	static bool IsPali(long x)
	{
		var s = x.ToString();
		var cs = s.ToCharArray();
		Array.Reverse(cs);
		var sr = new string(cs);
		return s == sr;
	}
}
