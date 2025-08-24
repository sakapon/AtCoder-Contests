class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c == '#').ToArray();
		var n = s.Length;

		var q = Enumerable.Range(1, n).Where(i => s[i - 1]).Chunk(2).Select(r => string.Join(",", r));
		return string.Join("\n", q);
	}
}
