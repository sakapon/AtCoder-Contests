class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		return s.GroupBy(c => c).OrderBy(g => -g.Count()).ThenBy(g => g.Key).First().Key;
	}
}
