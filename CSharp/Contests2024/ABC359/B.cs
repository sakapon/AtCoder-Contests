class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		return Enumerable.Range(0, 2 * n).GroupBy(i => a[i]).Count(g =>
		{
			var p = g.ToArray();
			return p[1] - p[0] == 2;
		});
	}
}
