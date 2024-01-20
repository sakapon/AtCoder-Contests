class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, k) = Read3();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var ao = new int[h, w];
		var ax = new int[h, w];

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				if (s[i][j] == 'o')
				{
					ao[i, j] = 1;
				}

				if (s[i][j] == 'x')
				{
					ax[i, j] = 1;
				}
			}
		}

		var so = new StaticRSQ2(ao);
		var sx = new StaticRSQ2(ax);
		var max = -1;

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j <= w - k; j++)
			{
				if (sx.GetSum(i, j, i + 1, j + k) > 0) continue;
				var v = (int)so.GetSum(i, j, i + 1, j + k);
				if (max < v) max = v;
			}
		}

		for (int j = 0; j < w; j++)
		{
			for (int i = 0; i <= h - k; i++)
			{
				if (sx.GetSum(i, j, i + k, j + 1) > 0) continue;
				var v = (int)so.GetSum(i, j, i + k, j + 1);
				if (max < v) max = v;
			}
		}

		if (max == -1) return -1;
		return k - max;
	}
}

public class StaticRSQ2
{
	long[,] s;
	public StaticRSQ2(int[,] a)
	{
		var n1 = a.GetLength(0);
		var n2 = a.GetLength(1);
		s = new long[n1 + 1, n2 + 1];
		for (int i = 0; i < n1; ++i)
		{
			for (int j = 0; j < n2; ++j) s[i + 1, j + 1] = s[i + 1, j] + a[i, j];
			for (int j = 1; j <= n2; ++j) s[i + 1, j] += s[i, j];
		}
	}

	public long GetSum(int l1, int l2, int r1, int r2) => s[r1, r2] - s[l1, r2] - s[r1, l2] + s[l1, l2];
}
