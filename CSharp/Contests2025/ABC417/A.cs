class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a, b) = Read3();
		var s = Console.ReadLine();
		return s[a..(n - b)];
	}
}
