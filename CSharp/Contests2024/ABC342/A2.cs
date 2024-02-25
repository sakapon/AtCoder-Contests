class A2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		if (s[0] != s[1] && s[0] != s[2]) return 1;
		return Enumerable.Range(1, s.Length).First(i => s[i] != s[0]) + 1;
	}
}
