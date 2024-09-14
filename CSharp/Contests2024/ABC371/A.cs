class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		if (s.Distinct().Count() == 2) return "B";
		return s[0] != s[2] ? "A" : "C";
	}
}
