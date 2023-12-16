class A
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		return new string(n.ToString()[0], n);
	}
}
