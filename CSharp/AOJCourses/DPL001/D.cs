using System;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new int[n], _ => int.Parse(Console.ReadLine()));
		Console.WriteLine(Lis(a));
	}

	static int Lis(int[] a)
	{
		var n = a.Length;
		var r = Array.ConvertAll(new bool[n + 1], _ => int.MaxValue);
		for (int i = 0; i < n; ++i)
			r[Min(0, n, x => r[x] >= a[i])] = a[i];
		return Array.IndexOf(r, int.MaxValue);
	}

	static int Min(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
