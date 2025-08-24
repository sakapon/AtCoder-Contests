class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = a.Distinct().OrderBy(x => x).ToArray();
		return $"{r.Length}\n" + string.Join(" ", r);
	}
}
