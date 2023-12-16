class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var ds = Math.Abs(s[0] - s[1]);
		var dt = Math.Abs(t[0] - t[1]);

		if (ds == 4) ds = 1;
		if (ds == 3) ds = 2;
		if (dt == 4) dt = 1;
		if (dt == 3) dt = 2;

		return ds == dt;
	}
}
