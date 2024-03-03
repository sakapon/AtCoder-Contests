class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = Read();
		return a.Sum() == 0 ? 1 : 0;
	}
}
