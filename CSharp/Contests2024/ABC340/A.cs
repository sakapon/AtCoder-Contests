class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, d) = Read3();

		var r = new List<int>();
		for (int x = a; x <= b; x += d)
			r.Add(x);
		return string.Join(" ", r);
	}
}
