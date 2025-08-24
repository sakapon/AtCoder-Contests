class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		s += "0";
		return n + Enumerable.Range(0, n).Sum(i => (s[i] - s[i + 1] + 10) % 10);
	}
}
