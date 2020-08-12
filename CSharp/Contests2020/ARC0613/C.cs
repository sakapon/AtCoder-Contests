using System;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var a = Read();

		for (int c = 0; c < k; c++)
		{
			var d = new int[n];
			for (int i = 0; i < n; i++)
			{
				d[Math.Max(0, i - a[i])]++;
				if (i + a[i] + 1 < n) d[i + a[i] + 1]--;
			}
			for (int i = 1; i < n; i++)
				d[i] += d[i - 1];

			if (Enumerable.SequenceEqual(a, d)) break;
			a = d;
		}
		Console.WriteLine(string.Join(" ", a));
	}
}
