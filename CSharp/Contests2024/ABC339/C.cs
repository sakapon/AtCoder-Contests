class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var sum = 0L;
		var min = 0L;

		foreach (var x in a)
		{
			sum += x;
			min = Math.Min(min, sum);
		}
		return sum - min;
	}
}
