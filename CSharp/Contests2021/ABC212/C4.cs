using System;

class C4
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var b = Read();

		Array.Sort(a);
		Array.Sort(b);

		var r = 1 << 30;
		for (var (i, j) = (0, 0); i < n && j < m;)
		{
			if (a[i] == b[j]) return 0;

			var d = a[i] - b[j];
			if (d > 0)
			{
				r = Math.Min(r, d);
				j++;
			}
			else
			{
				r = Math.Min(r, -d);
				i++;
			}
		}
		return r;
	}
}
