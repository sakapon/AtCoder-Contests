using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (x, y, z) = Read3();
		var s = Console.ReadLine();

		var (r0, r1) = (0L, 1L << 60);
		foreach (var c in s)
			(r0, r1) = c == 'a' ? (Math.Min(r0, r1 + z) + x, Math.Min(r0 + z, r1) + y) : (Math.Min(r0, r1 + z) + y, Math.Min(r0 + z, r1) + x);
		return Math.Min(r0, r1);
	}
}
