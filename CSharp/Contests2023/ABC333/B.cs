class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var ds = Math.Abs(s[0] - s[1]);
		var dt = Math.Abs(t[0] - t[1]);

		var a14 = new[] { 1, 4 };
		return a14.Contains(ds) == a14.Contains(dt);
	}
}
