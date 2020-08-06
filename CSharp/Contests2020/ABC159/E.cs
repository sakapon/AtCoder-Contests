using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		var z = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int h = z[0], w = z[1], k = z[2];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var m = 1 << 30;
		var bits = 1 << (h - 1);

		for (int x = 0; x < bits; x++)
			m = Math.Min(m, ForBit(x));
		Console.WriteLine(m);

		int ForBit(int x)
		{
			var c = 0;
			var map = new Dictionary<int, int>();
			var mi = 0;
			for (int i = 0; i < h; i++)
			{
				map[i] = mi;
				if ((x & (1 << i)) != 0) { mi++; c++; }
			}

			// 累積和
			var whites = new int[mi + 1, w + 1];
			for (int j = 0; j < w; j++)
			{
				for (int i = 0; i <= mi; i++)
					whites[i, j + 1] = whites[i, j];
				for (int i = 0; i < h; i++)
					if (s[i][j] == '1') whites[map[i], j + 1]++;
			}

			var tj = 0;
			for (int j = 1; j <= w; j++)
			{
				for (int i = 0; i <= mi; i++)
				{
					if (whites[i, j] - whites[i, tj] <= k) continue;
					if (tj == j - 1) return 1 << 30;
					tj = j - 1;
					c++;
				}
			}
			return c;
		}
	}
}
