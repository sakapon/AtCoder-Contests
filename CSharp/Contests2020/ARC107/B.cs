using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();
		var n2 = 2 * n;

		long Count2(int x) => n - Math.Abs(n + 1 - x);

		var r = 0L;
		for (int cd = 2; cd <= n2; cd++)
		{
			var ab = cd + k;
			if (ab < 2 || n2 < ab) continue;
			r += Count2(ab) * Count2(cd);
		}
		Console.WriteLine(r);
	}
}
