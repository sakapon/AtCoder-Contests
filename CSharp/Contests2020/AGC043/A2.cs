using System;
using System.Linq;

class A2
{
	static void Main()
	{
		var z = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = z[0], w = z[1];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var u = new int[h, w];
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				u[i, j] = 1 << 30;
		u[0, 0] = s[0][0] == '.' ? 0 : 1;

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				if (i > 0) u[i, j] = Math.Min(u[i, j], u[i - 1, j] + (s[i - 1][j] > s[i][j] ? 1 : 0));
				if (j > 0) u[i, j] = Math.Min(u[i, j], u[i, j - 1] + (s[i][j - 1] > s[i][j] ? 1 : 0));
			}
		Console.WriteLine(u[h - 1, w - 1]);
	}
}
