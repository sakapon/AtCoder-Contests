using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var p = read();
		int n = h[0], k = h[1];

		var smin = Slide(p, k + 1, (x, y) => x <= y);
		var smax = Slide(p, k + 1, (x, y) => x >= y);
		var c = n - k + 1 - Enumerable.Range(0, n - k).Count(i => smin[i] == p[i] && smax[i] == p[i + k]);

		int c2 = 0, t = 1;
		for (int i = 1; i < n; i++)
		{
			if (p[i] > p[i - 1]) t++;
			else
			{
				if (t >= k) c2++;
				t = 1;
			}
		}
		if (t >= k) c2++;

		Console.WriteLine(c - Math.Max(c2 - 1, 0));
	}

	static int[] Slide(int[] a, int k, Func<int, int, bool> c)
	{
		var r = new int[a.Length - k + 1];
		var q = new int[a.Length];
		for (int i = 1 - k, j = 0, s = 0, t = -1; j < a.Length; i++, j++)
		{
			while (s <= t && c(a[j], a[q[t]])) t--;
			q[++t] = j;
			if (i < 0) continue;
			r[i] = a[q[s]];
			if (q[s] == i) s++;
		}
		return r;
	}
}
