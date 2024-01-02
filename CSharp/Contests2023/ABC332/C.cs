class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = Console.ReadLine();

		return s.Split('0').Max(t => Math.Max(t.Count(c => c == '2'), t.Length - m));
	}
}
