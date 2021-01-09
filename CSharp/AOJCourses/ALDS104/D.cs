using System;
using System.Linq;

class D
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var k = h[1];
		var w = new int[h[0]].Select(_ => int.Parse(Console.ReadLine())).ToArray();

		Console.WriteLine(First(0, 1 << 30, p =>
		{
			int c = 1, t = 0;
			foreach (var v in w)
			{
				if (t + v > p)
				{
					c++;
					t = v;
				}
				else
				{
					t += v;
				}
				if (v > p || c > k) return false;
			}
			return true;
		}));
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
