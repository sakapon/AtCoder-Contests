class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = Read();
		var c = Console.ReadLine();

		if (c == "Red") return Math.Min(a[1], a[2]);
		if (c == "Green") return Math.Min(a[0], a[2]);
		return Math.Min(a[0], a[1]);
	}
}
