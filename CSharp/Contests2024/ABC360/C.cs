class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var w = Read();

		return Enumerable.Range(0, n)
			.GroupBy(i => a[i])
			.Select(g => g.Select(i => w[i]).ToArray())
			.Sum(g => g.Sum() - g.Max());
	}
}
