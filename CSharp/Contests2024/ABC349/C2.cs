class C2
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine().ToLower();

		var i = s.IndexOf(t[0]);
		if (i == -1) return false;

		i = s.IndexOf(t[1], i + 1);
		if (i == -1) return false;

		if (t[2] == 'x') return true;
		i = s.IndexOf(t[2], i + 1);
		return i != -1;
	}
}
