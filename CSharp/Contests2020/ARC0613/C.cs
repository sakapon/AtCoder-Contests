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

		var next = true;
		for (int c = 0; c < k && next; c++)
		{
			next = false;

			var d = new int[n + 1];
			for (int i = 0; i < n; i++)
			{
				d[Math.Max(0, i - a[i])]++;
				d[Math.Min(n, i + a[i] + 1)]--;
			}

			var t = 0;
			for (int i = 0; i < n; i++)
			{
				t += d[i];
				if (a[i] != t)
				{
					a[i] = t;
					next = true;
				}
			}
		}
		Console.WriteLine(string.Join(" ", a));
	}
}
