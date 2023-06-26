using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		const string snuke = "snuke";
		var r5 = Enumerable.Range(0, 5).ToArray();
		var m = Math.Min(h, w);

		var (xs, ys) = CheckAll();
		return string.Join("\n", r5.Select(i => $"{xs[i] + 1} {ys[i] + 1}"));

		(int[], int[]) CheckAll()
		{
			int[] xs, ys;
			bool Check() => r5.All(k => s[xs[k]][ys[k]] == snuke[k]);
			void Next(int dx, int dy)
			{
				for (int k = 0; k < 5; k++)
				{
					xs[k] += dx;
					ys[k] += dy;
				}
			}

			for (int i = 0; i < h; i++)
			{
				xs = Enumerable.Repeat(i, 5).ToArray();
				ys = new[] { 0, 1, 2, 3, 4 };

				for (int j = 0; j < w - 4; j++)
				{
					if (Check()) return (xs, ys);
					Next(0, 1);
				}

				Array.Reverse(ys);

				for (int j = 0; j < w - 4; j++)
				{
					Next(0, -1);
					if (Check()) return (xs, ys);
				}
			}

			for (int j = 0; j < w; j++)
			{
				xs = new[] { 0, 1, 2, 3, 4 };
				ys = Enumerable.Repeat(j, 5).ToArray();

				for (int i = 0; i < h - 4; i++)
				{
					if (Check()) return (xs, ys);
					Next(1, 0);
				}

				Array.Reverse(xs);

				for (int i = 0; i < h - 4; i++)
				{
					Next(-1, 0);
					if (Check()) return (xs, ys);
				}
			}

			for (int i = 0; i < h - 4; i++)
			{
				xs = new[] { i, i + 1, i + 2, i + 3, i + 4 };
				ys = new[] { 0, 1, 2, 3, 4 };

				for (int j = 0; j < w - 4; j++)
				{
					if (Check()) return (xs, ys);
					Next(0, 1);
				}

				Array.Reverse(xs);
				Array.Reverse(ys);

				for (int j = 0; j < w - 4; j++)
				{
					Next(0, -1);
					if (Check()) return (xs, ys);
				}
			}

			for (int i = 0; i < h - 4; i++)
			{
				xs = new[] { i + 4, i + 3, i + 2, i + 1, i };
				ys = new[] { 0, 1, 2, 3, 4 };

				for (int j = 0; j < w - 4; j++)
				{
					if (Check()) return (xs, ys);
					Next(0, 1);
				}

				Array.Reverse(xs);
				Array.Reverse(ys);

				for (int j = 0; j < w - 4; j++)
				{
					Next(0, -1);
					if (Check()) return (xs, ys);
				}
			}

			throw new InvalidOperationException();
		}
	}
}
