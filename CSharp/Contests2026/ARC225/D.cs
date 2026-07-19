class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		return p.Select((x, i) => x - 1 - i).Sum(x => (long)Math.Abs(x)) / 2;
	}
}
