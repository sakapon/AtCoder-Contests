using System;
using System.Linq;

class E
{
	static void Main()
	{
		var z = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int h = z[0], w = z[1];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();

		const long M = 1000000007;
		var k = s.Sum(t => t.Count(c => c == '.'));
		var p2 = new long[k + 1];
		p2[0] = 1;
		for (int i = 0; i < k; ++i) p2[i + 1] = p2[i] * 2 % M;

		var u = new int[h, w];

		for (int i = 0; i < h; i++)
		{
			var start = -1;
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '.')
				{
					if (start == -1)
					{
						start = j;
					}
				}
				else
				{
					if (start != -1)
					{
						var end = j;
						for (int x = start; x < end; x++)
						{
							u[i, x] += end - start;
						}
						start = -1;
					}
				}
			}
			if (start != -1)
			{
				var end = w;
				for (int x = start; x < end; x++)
				{
					u[i, x] += end - start;
				}
				start = -1;
			}
		}

		for (int j = 0; j < w; j++)
		{
			var start = -1;
			for (int i = 0; i < h; i++)
			{
				if (s[i][j] == '.')
				{
					if (start == -1)
					{
						start = i;
					}
				}
				else
				{
					if (start != -1)
					{
						var end = i;
						for (int x = start; x < end; x++)
						{
							u[x, j] += end - start;
						}
						start = -1;
					}
				}
			}
			if (start != -1)
			{
				var end = h;
				for (int x = start; x < end; x++)
				{
					u[x, j] += end - start;
				}
				start = -1;
			}
		}

		var r = 0L;
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == '#') continue;
				r = (r + (p2[u[i, j] - 1] - 1 + M) % M * p2[k - u[i, j] + 1]) % M;
			}
		}
		Console.WriteLine(r);
	}
}
