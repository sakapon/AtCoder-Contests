class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var r = n;
		s += "0";

		for (int i = 0; i < n; i++)
			r += (s[i] - s[i + 1] + 10) % 10;
		return r;
	}
}
