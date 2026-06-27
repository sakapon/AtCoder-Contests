using static System.Linq.Enumerable;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		int[] rh = [.. Range(0, h)];
		int[] rw = [.. Range(0, w)];

		var iMin = rh.Where(i => c[i].Contains('#')).Min();
		var iMax = rh.Where(i => c[i].Contains('#')).Max();
		iMax++;

		var jMin = rw.Where(j => rh.Any(i => c[i][j] == '#')).Min();
		var jMax = rw.Where(j => rh.Any(i => c[i][j] == '#')).Max();
		jMax++;

		var r = c[iMin..iMax].Select(s => s[jMin..jMax]);
		return string.Join("\n", r);
	}
}
