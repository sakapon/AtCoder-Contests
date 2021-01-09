using System;

class I
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var seq = new Seq(a);
		var sum = seq.Sum(..);
		var half = sum / 2;

		var min = 1L << 60;

		for (int i = 0; i < n; i++)
		{
			var j = First(0, n, x => 2 * seq.Sum(i..x) >= sum);

			min = Math.Min(min, Math.Abs(sum - 2 * seq.Sum(i..j)));
			min = Math.Min(min, Math.Abs(sum - 2 * seq.Sum(i..(j - 1))));
		}

		Console.WriteLine(min);
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}

class Seq
{
	int[] a;
	long[] s;
	public Seq(int[] _a) { a = _a; }

	public long Sum(int minIn, int maxEx)
	{
		if (s == null)
		{
			s = new long[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		}
		return s[maxEx] - s[minIn];
	}

	// C# 8.0
	public long Sum(Range r) => Sum(r.Start.GetOffset(a.Length), r.End.GetOffset(a.Length));
}
