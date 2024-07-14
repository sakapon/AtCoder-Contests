class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (xa, ya) = Read2();
		var (xb, yb) = Read2();
		var (xc, yc) = Read2();

		return
			(xb - xa) * (xc - xa) + (yb - ya) * (yc - ya) == 0 ||
			(xc - xb) * (xa - xb) + (yc - yb) * (ya - yb) == 0 ||
			(xa - xc) * (xb - xc) + (ya - yc) * (yb - yc) == 0;
	}
}
