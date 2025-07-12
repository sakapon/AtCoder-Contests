class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, L, R) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		return ps.Count(p => p.x <= L && R <= p.y);
	}
}
