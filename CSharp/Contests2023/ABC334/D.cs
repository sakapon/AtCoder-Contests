using CoderLib8.Collections.Statics.Int;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var r = ReadL();
		var qs = Array.ConvertAll(new bool[qc], _ => long.Parse(Console.ReadLine()));

		Array.Sort(r);

		var set = new IntCumCountSet(r);
		return string.Join("\n", qs.Select(x => set.GetAt(x)));
	}
}
