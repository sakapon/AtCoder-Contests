using CoderLib8.DataTrees.SBTs;
using CoderLib8.Values;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = (IntV)Read2L();
		var ps = Array.ConvertAll(new bool[n], _ => (IntV)Read2L());

		var rn = Enumerable.Range(0, n - 1).ToArray();
		var ds = rn.Select(i => (ps[i + 1] - ps[i]).Norm).ToArray();
		var es = rn.Select(i => (ps[i + 1] - s).Norm + (ps[i] - s).Norm - ds[i]).ToArray();

		var monoid = new Monoid<double>((x, y) => x <= y ? x : y, double.MaxValue);
		var st = new MergeSBT<double>(n, monoid);
		st[0] = 0;

		for (int i = 1; i < n; i++)
		{
			st[i] = st[i - k, i] + es[i - 1];
		}
		return ds.Sum() + (ps[0] - s).Norm + (ps[^1] - s).Norm + st[n - k, n];
	}
}
