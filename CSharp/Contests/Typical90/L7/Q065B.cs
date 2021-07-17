using System;

class Q065B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (R, G, B, K) = Read4();
		var (X, Y, Z) = Read3();

		var c = 0L;
		var mc = new MCombination(R + G + B);

		for (int r = 0; r <= R; r++)
		{
			for (int g = 0; g <= G && g <= K - r; g++)
			{
				var b = K - r - g;
				if (b > B) continue;

				if (r + g > X) continue;
				if (g + b > Y) continue;
				if (b + r > Z) continue;

				c += mc.MNcr(R, r) * mc.MNcr(G, g) % M * mc.MNcr(B, b);
				c %= M;
			}
		}
		return c;
	}

	const long M = 998244353;

	// MCombination の M を変更します。
}
