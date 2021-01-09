using System;
using System.IO;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var z = Read();
		int h = z[0], w = z[1];
		var s = new int[h].Select(_ => Console.ReadLine()).ToArray();
		z = Read();
		int r = z[0], c = z[1];
		var p = new int[r].Select(_ => Console.ReadLine()).ToArray();
		if (h < r || w < c) return;

		long M1 = 1000000007, M2 = 10007;
		var M1_Pow = Pow(M1, r);
		var M2_Pow = Pow(M2, c);

		var M2_Pows = new long[c + 1];
		M2_Pows[0] = 1;
		for (int j = 0; j < c; j++)
			M2_Pows[j + 1] = M2_Pows[j] * M2;

		// j に対する i 方向のハッシュ (M2 なし)
		var t = new long[w];
		for (int i = 0; i < r; i++)
			for (int j = 0; j < w; j++)
				t[j] = M1 * t[j] + s[i][j];

		long sh = 0, ph = 0;
		for (int j = 0; j < c; j++)
			sh = M2 * sh + t[j];
		for (int i = 0; i < r; i++)
		{
			long phc = 0;
			for (int j = 0; j < c; j++)
				phc = M2 * phc + p[i][j];
			ph = M1 * ph + phc;
		}

		for (int i = 0; i + r - 1 < h; i++)
		{
			for (int j = 0; j + c - 1 < w; j++)
			{
				// 厳密な比較は省略
				if (sh == ph) Console.WriteLine($"{i} {j}");
				if (j + c < w) sh = M2 * sh - M2_Pow * t[j] + t[j + c];
			}

			sh = 0;
			if (i + r < h)
			{
				for (int j = 0; j < w; j++)
					t[j] = M1 * t[j] - M1_Pow * s[i][j] + s[i + r][j];
				for (int j = 0; j < c; j++)
					sh = M2 * sh + t[j];
			}
		}
		Console.Out.Flush();
	}

	static long Pow(long b, long i)
	{
		for (var r = 1L; ; b *= b)
		{
			if ((i & 1) != 0) r *= b;
			if ((i >>= 1) == 0) return r;
		}
	}
}
