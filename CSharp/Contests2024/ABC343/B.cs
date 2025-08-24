class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Read());

		return string.Join("\n", a.Select(r => string.Join(" ", Enumerable.Range(1, n).Where(j => r[j - 1] == 1))));
	}
}
