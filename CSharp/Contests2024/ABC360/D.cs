using CoderLib8.Collections.Statics.Typed;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, t) = Read2L();
		var s = Console.ReadLine();
		var x = ReadL();

		var rn = Enumerable.Range(0, (int)n).ToArray();
		var x0 = rn.Where(i => s[i] == '0').Select(i => x[i]).ToArray();
		var x1 = rn.Where(i => s[i] == '1').Select(i => x[i]).ToArray();

		Array.Sort(x0);
		var set = new ArrayItemSet<long>(x0);
		return x1.Sum(v => (long)set.GetCount(v + 1, v + 2 * t + 1));
	}
}
