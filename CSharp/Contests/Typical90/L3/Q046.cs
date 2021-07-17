using System;

class Q046
{
	const int M = 46;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();
		var c = Read();

		var ca = Tally(a);
		var cb = Tally(b);
		var cc = Tally(c);

		var r = Convolve(ca, cb);
		r = Convolve(r, cc);
		return r[0];
	}

	static long[] Tally(int[] a)
	{
		var r = new long[M];
		foreach (var x in a) ++r[x % M];
		return r;
	}

	static long[] Convolve(long[] a, long[] b)
	{
		var r = new long[M];
		for (int i = 0; i < M; i++)
			for (int j = 0; j < M; j++)
				r[(i + j) % M] += a[i] * b[j];
		return r;
	}
}
