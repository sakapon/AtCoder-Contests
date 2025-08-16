class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int r, int c) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var minR = ps.Min(p => p.r);
		var maxR = ps.Max(p => p.r);
		var minC = ps.Min(p => p.c);
		var maxC = ps.Max(p => p.c);

		return (Math.Max(maxR - minR, maxC - minC) + 1) / 2;
	}
}
