class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var c0 = s.GroupBy(c => c).Single(g => g.Count() == 1).Key;
		return s.IndexOf(c0) + 1;
	}
}
