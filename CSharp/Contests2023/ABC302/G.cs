using System;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var b = (int[])a.Clone();
		Array.Sort(b);

		var map = new int[4, 4];
		for (int i = 0; i < n; i++)
		{
			map[b[i] - 1, a[i] - 1]++;
		}

		var r = 0;

		// 位数 1
		for (int i = 0; i < 4; i++)
		{
			map[i, i] = 0;
		}

		// 位数 2
		for (int i = 0; i < 4; i++)
		{
			for (int j = i + 1; j < 4; j++)
			{
				var c = Math.Min(map[i, j], map[j, i]);
				r += c;
				map[i, j] -= c;
				map[j, i] -= c;
			}
		}

		// 位数 3
		for (int i = 0; i < 4; i++)
		{
			for (int j = i + 1; j < 4; j++)
			{
				for (int k = j + 1; k < 4; k++)
				{
					var c = Math.Min(Math.Min(map[i, j], map[j, k]), map[k, i]);
					r += c * 2;
					map[i, j] -= c;
					map[j, k] -= c;
					map[k, i] -= c;

					c = Math.Min(Math.Min(map[i, k], map[k, j]), map[j, i]);
					r += c * 2;
					map[i, k] -= c;
					map[k, j] -= c;
					map[j, i] -= c;
				}
			}
		}

		// 位数 4
		{
			var c = 0;
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					c += map[i, j];
			r += c / 4 * 3;
		}

		return r;
	}
}
