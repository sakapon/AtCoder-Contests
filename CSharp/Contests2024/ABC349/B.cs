class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		return s.GroupBy(c => c).GroupBy(g => g.Count()).All(g => g.Count() == 2);
	}
}
