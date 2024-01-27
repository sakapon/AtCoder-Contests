class A
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		return char.IsUpper(s[0]) && s[1..].All(c => char.IsLower(c));
	}
}
