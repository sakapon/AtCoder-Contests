class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		return string.Join("0", Enumerable.Repeat("1", n + 1));
	}
}
