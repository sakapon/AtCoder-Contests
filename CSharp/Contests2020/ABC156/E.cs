using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, k) = Read2();

		var ncrs = MNcrs(n);
		var m_max = Math.Min(n - 1, k);

		MInt r = 0;
		for (int m = 0; m <= m_max; m++)
			r += ncrs[m] * ncrs[m] * (n - m);
		Console.WriteLine(r / n);
	}

	static MInt[] MNcrs(int n)
	{
		var c = new MInt[n + 1];
		c[0] = 1;
		for (int i = 0; i < n; ++i) c[i + 1] = c[i] * (n - i) / (i + 1);
		return c;
	}
}
