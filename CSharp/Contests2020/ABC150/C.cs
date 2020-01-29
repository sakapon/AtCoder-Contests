using System;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		n = int.Parse(Console.ReadLine());
		f = new int[n + 1];
		f[0] = 1;
		for (int i = 1; i <= n; i++)
			f[i] = i * f[i - 1];

		var p = GetOrder(Read());
		var q = GetOrder(Read());
		Console.WriteLine(Math.Abs(p - q));
	}

	static int n;
	static int[] f;
	static int GetOrder(int[] p)
	{
		var pi = 0;
		var pl = Enumerable.Range(1, n).ToList();
		for (int i = 0; i < n; i++)
		{
			var li = pl.IndexOf(p[i]);
			pi += li * f[n - 1 - i];
			pl.RemoveAt(li);
		}
		return pi;
	}
}
