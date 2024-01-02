using System;

class C3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		Array.Sort(a);
		var r = 0;

		for (int i = 0, j = 0; i < n; i++)
		{
			while (j < n && a[j] < a[i] + m) j++;
			r = Math.Max(r, j - i);
		}
		return r;
	}
}
