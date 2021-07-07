using System;

class Q081A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		var max = 5000;
		var raq = new StaticRAQ2(max + 1, max + 1);

		foreach (var (a, b) in ps)
		{
			raq.Add(a, b, a + k + 1, b + k + 1, 1);
		}
		var all = raq.GetAll0();

		var r = 0L;
		for (int i = 0; i <= max; i++)
		{
			for (int j = 0; j <= max; j++)
			{
				r = Math.Max(r, all[i, j]);
			}
		}
		return r;
	}
}
