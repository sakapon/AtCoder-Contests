using System;
using System.Linq;

class C
{
	static void Main()
	{
		n = int.Parse(Console.ReadLine());
		f = new int[n + 1];
		f[0] = 1;
		for (int i = 1; i <= n; i++) f[i] = i * f[i - 1];

		Console.WriteLine(Math.Abs(Order() - Order()));
	}

	static int n;
	static int[] f;

	static int Order()
	{
		var p = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var l = Enumerable.Range(1, n).ToList();
		var r = 0;
		for (int i = 0; i < n; i++)
		{
			r += l.IndexOf(p[i]) * f[n - 1 - i];
			l.Remove(p[i]);
		}
		return r;
	}
}
