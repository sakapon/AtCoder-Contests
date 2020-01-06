using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).GroupBy(x => x).Select(g => g.Count()).OrderBy(x => -x).ToArray();

		var f = new int[n + 1];
		for (int i = a.Length, c = 1; c <= n; c++)
		{
			while (i > 0 && a[i - 1] < c) i--;
			f[c] = f[c - 1] + i;
		}
		f[0] = int.MaxValue;
		for (int c = 1; c <= n; c++) f[c] /= c;

		var r = new int[n + 1];
		for (int c = n, i = 1; i <= n; i++)
		{
			while (f[c] < i) c--;
			r[i] = c;
		}
		Console.WriteLine(string.Join("\n", r.Skip(1)));
	}
}
