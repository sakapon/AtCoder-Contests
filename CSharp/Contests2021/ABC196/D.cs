using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, a, b) = Read4();

		// 縦棒の個数
		var hc = h * (w - 1);
		// 横棒の個数
		var wc = (h - 1) * w;
		var c = hc + wc;

		// 不可能なとき、true
		var u = new bool[c, c];

		//int ToId(int i, int j) => i * w + j;

		// 縦棒同士
		for (int hi = 0; hi < h; hi++)
		{
			for (int hj = 0; hj < w - 2; hj++)
			{
				var hid = hi * (w - 1) + hj;

				u[hid, hid + 1] = true;
				u[hid + 1, hid] = true;
			}
		}

		// 横棒同士
		for (int wi = 0; wi < h - 2; wi++)
		{
			for (int wj = 0; wj < w; wj++)
			{
				var wid = wi * w + wj;
				wid += hc;

				u[wid, wid + w] = true;
				u[wid + w, wid] = true;
			}
		}

		// 縦棒と横棒
		for (int hi = 0; hi < h; hi++)
		{
			for (int hj = 0; hj < w - 1; hj++)
			{
				var hid = hi * (w - 1) + hj;

				for (int wi = 0; wi < h - 1; wi++)
				{
					for (int wj = 0; wj < w; wj++)
					{
						var wid = wi * w + wj;
						wid += hc;

						if ((hi == wi || hi == wi + 1) && (hj == wj || hj + 1 == wj))
						{
							u[hid, wid] = true;
							u[wid, hid] = true;
						}
					}
				}
			}
		}

		var r = 0L;

		Combination(Enumerable.Range(0, c).ToArray(), a, p =>
		{
			for (int i = 0; i < a; i++)
			{
				for (int j = i + 1; j < a; j++)
				{
					if (u[p[i], p[j]])
					{
						return;
					}
				}
			}

			r++;
		});

		return r;
	}

	public static void Combination<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0, 0);
		else action(p);

		void Dfs(int i, int j0)
		{
			var i2 = i + 1;
			for (int j = j0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2, j + 1);
				else action(p);
			}
		}
	}
}
