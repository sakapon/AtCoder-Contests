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

		var c = n - k + 1;
		c -= Enumerable.Range(0, n - k).Count(i => p.Skip(i).Take(k + 1).Min() == p[i] && p.Skip(i).Take(k + 1).Max() == p[i + k]);

		int c2 = 0, t = 0, v = -1;
		for (int i = 0; i < n; i++)
		{
			if (p[i] > v)
			{
				t++;
			}
			else
			{
				if (t >= k) c2++;
				t = 1;
			}
			v = p[i];
		}
		if (t >= k) c2++;
		if (c2 > 0) c -= c2 - 1;

		Console.WriteLine(c);
	}
}
