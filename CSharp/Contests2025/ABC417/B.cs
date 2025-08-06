class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read().ToList();
		var b = Read();

		foreach (var v in b)
			a.Remove(v);
		return string.Join(" ", a);
	}
}
