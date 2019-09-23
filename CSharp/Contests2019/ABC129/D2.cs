using System;
using System.Linq;

class D2
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = a[0], w = a[1];
		var s = new int[h].Select(_ => Console.ReadLine().Select(x => x == '#' ? -1 : 0).ToArray()).ToArray();

		for (int i = 0; i < h; i++)
		{
			for (int t = 0, j = 0; j < w; j++)
				if (s[i][j] == -1) t = 0;
				else s[i][j] += t++;
			for (int t = 0, j = w - 1; j >= 0; j--)
				if (s[i][j] == -1) t = 0;
				else s[i][j] += t++;
		}
		for (int j = 0; j < w; j++)
		{
			for (int t = 0, i = 0; i < h; i++)
				if (s[i][j] == -1) t = 0;
				else s[i][j] += t++;
			for (int t = 0, i = h - 1; i >= 0; i--)
				if (s[i][j] == -1) t = 0;
				else s[i][j] += t++;
		}
		Console.WriteLine(s.SelectMany(x => x).Max() + 1);
	}
}
