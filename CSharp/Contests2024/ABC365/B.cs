class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		return a.Select((v, i) => (v, i)).OrderBy(p => -p.v).ElementAt(1).i + 1;
	}
}
