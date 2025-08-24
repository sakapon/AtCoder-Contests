class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		return s.TrimEnd('C').TrimEnd('B').TrimEnd('A') == "";
	}
}
