using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var c = Read();

		var b = new int[m + 1];

		for (int j = m; j >= 0; j--)
		{
			var t = c[j + n];
			for (int i = Math.Min(n - 1, j + n); i >= 0 && j + n - i <= m; i--)
			{
				t -= a[i] * b[j + n - i];
			}
			b[j] = t / a[n];
		}
		return string.Join(" ", b);
	}
}
