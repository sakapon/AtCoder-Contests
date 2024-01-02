class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		return new string(s[0], int.Parse(s));
	}
}
