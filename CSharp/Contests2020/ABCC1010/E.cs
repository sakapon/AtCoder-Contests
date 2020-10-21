using System;
using System.Linq;

class E
{
	const long M = 1000000007;
	static void Main()
	{
		var z = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int h = z[0], w = z[1];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();

		var k = s.Sum(t => t.Count(c => c == '.'));
		var p2 = new long[k + 1];
		p2[0] = 1;
		for (int i = 0; i < k; ++i) p2[i + 1] = p2[i] * 2 % M;

		var u = new int[h, w];

		for (int i = 0; i < h; i++)
		{
			var start = -1;
			for (int j = 0; j <= w; j++)
			{
				if (j < w && s[i][j] == '.')
				{
					if (start == -1) start = j;
				}
				else
				{
					if (start != -1) for (int x = start; x < j; x++) u[i, x] += j - start;
					start = -1;
				}
			}
		}
		for (int j = 0; j < w; j++)
		{
			var start = -1;
			for (int i = 0; i <= h; i++)
			{
				if (i < h && s[i][j] == '.')
				{
					if (start == -1) start = i;
				}
				else
				{
					if (start != -1) for (int x = start; x < i; x++) u[x, j] += i - start;
					start = -1;
				}
			}
		}

		var r = 0L;
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (u[i, j] > 0)
					r = (r + (p2[u[i, j] - 1] - 1 + M) % M * p2[k - u[i, j] + 1]) % M;
		Console.WriteLine(r);
	}
}
