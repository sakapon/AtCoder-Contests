class A
{
	static void Main() => Console.WriteLine(Solve() ? "East" : "West");
	static bool Solve()
	{
		var s = Console.ReadLine();

		var w = s.Count(c => c == 'W');
		var e = s.Length - w;
		return w < e;
	}
}
