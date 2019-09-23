using System;
using System.Linq;

class D
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = a[0], w = a[1];
		var s = new int[h].Select(_ => Console.ReadLine().Select(x => x == '#' ? -1 : 0).ToArray()).ToArray();

		for (int i = 0; i < h; i++)
		{
			var t = -1;
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == -1)
				{
					if (t != -1) for (int k = t; k < j; k++) s[i][k] += j - t;
					t = -1;
				}
				else if (t == -1) t = j;
			}
			if (t != -1) for (int k = t; k < w; k++) s[i][k] += w - t;
		}
		for (int j = 0; j < w; j++)
		{
			var t = -1;
			for (int i = 0; i < h; i++)
			{
				if (s[i][j] == -1)
				{
					if (t != -1) for (int k = t; k < i; k++) s[k][j] += i - t;
					t = -1;
				}
				else if (t == -1) t = i;
			}
			if (t != -1) for (int k = t; k < h; k++) s[k][j] += h - t;
		}
		Console.WriteLine(s.SelectMany(x => x).Max() - 1);
	}
}
