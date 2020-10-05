using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], s = h[1];
		var a = Read();
		var seq = new Seq(a);

		var m = 1 << 30;
		for (int l = 0, r = 0; l < n; l++)
		{
			while (r <= n && seq.Sum(l, r) < s) r++;
			if (r > n) break;
			Chmin(ref m, r - l);
		}
		Console.WriteLine(m == 1 << 30 ? 0 : m);
	}

	static int Chmin(ref int x, int v) => x > v ? x = v : x;
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
}
