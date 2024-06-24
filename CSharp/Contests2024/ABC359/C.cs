class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (sx, sy) = Read2L();
		var (tx, ty) = Read2L();

		var min = (sx + sy) % 2 == 0 ? sx : sx - 1;
		var max = (sx + sy) % 2 == 0 ? sx + 1 : sx;

		var r = Math.Abs(sy - ty);
		min -= r;
		max += r;

		if (min <= tx && tx <= max) return r;
		if (tx < min) return r + (min - tx + 1) / 2;
		return r + (tx - max + 1) / 2;
	}
}
