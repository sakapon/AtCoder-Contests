using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var (r0, r1) = (0L, 0L);

		foreach (var (x, y) in ps)
		{
			if (x == 0)
			{
				Chmax(ref r0, Math.Max(r0, r1) + y);
			}
			else
			{
				Chmax(ref r1, r0 + y);
			}
		}

		return Math.Max(r0, r1);
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}
