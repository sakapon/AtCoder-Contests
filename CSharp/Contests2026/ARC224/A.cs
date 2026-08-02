class A
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var k = long.Parse(Console.ReadLine());
		return Enumerable.Range(1, 100)
			.Select(i => (i * k).ToString())
			.First(s => s.Contains("00"));
	}
}
