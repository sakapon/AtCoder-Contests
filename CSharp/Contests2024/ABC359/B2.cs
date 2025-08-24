class B2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		return Enumerable.Range(0, 2 * n - 2).Count(i => a[i] == a[i + 2]);
	}
}
