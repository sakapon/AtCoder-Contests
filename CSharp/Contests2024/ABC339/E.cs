using CoderLib8.DataTrees.SBTs;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();
		var a = Read();

		const int amax = 500000;
		var monoid = new Monoid<int>((x, y) => x >= y ? x : y, 0);
		var st = new MergeSBT<int>(amax + 1, monoid);

		foreach (var v in a)
		{
			var max = st[v - d, v + d + 1] + 1;
			st[v] = Math.Max(st[v], max);
		}
		return st[0, amax + 1];
	}
}
