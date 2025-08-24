class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var s1 = ps.Sum(p => p.Item1);
		var s2 = ps.Sum(p => p.Item2);
		return s1 > s2 ? "Takahashi" : s1 == s2 ? "Draw" : "Aoki";
	}
}
