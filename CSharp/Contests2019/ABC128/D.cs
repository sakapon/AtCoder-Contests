using System;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var v = read();
		int n = h[0], k = h[1], nk = Math.Min(n, k), M = 0;

		for (int i = 1; i <= nk; i++)
		{
			var t = v.Take(i).ToArray();
			var neg = t.Count(x => x < 0);
			M = Math.Max(M, t.OrderBy(x => x).Skip(Math.Min(neg, k - i)).Sum());

			for (int j = 1; j <= i; j++)
			{
				if (t[i - j] < 0) neg--;
				if (v[n - j] < 0) neg++;
				t[i - j] = v[n - j];
				M = Math.Max(M, t.OrderBy(x => x).Skip(Math.Min(neg, k - i)).Sum());
			}
		}
		Console.WriteLine(M);
	}
}
