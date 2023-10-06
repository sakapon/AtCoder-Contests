using System;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var r = new int[n];
		Array.Fill(r, -1);
		foreach (var i in a)
			r[i - 1] = 0;
		for (int i = n - 2; i >= 0; i--)
			if (r[i] == -1) r[i] = r[i + 1] + 1;
		return string.Join("\n", r);
	}
}
