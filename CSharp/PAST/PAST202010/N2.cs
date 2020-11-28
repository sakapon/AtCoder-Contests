using System;
using System.Linq;

class N2
{
	static void Main()
	{
		var s = new bool[18].Select(_ => Console.ReadLine().Select(c => c - '0').ToArray()).ToList();
		s.Insert(0, new int[6]);
		s.Insert(0, new int[6]);
		s.Add(new int[6]);
		s.Add(new int[6]);

		var valid = Array.ConvertAll(new bool[1 << 18], _ => true);
		for (int x = 0; x < 1 << 18; x++)
		{
			var a = NewArray2<int>(3, 8);
			for (int xi = 0; xi < 18; xi++)
				a[xi / 6][xi % 6 + 1] = (x >> xi) & 1;

			for (int j = 1; j <= 6; j++)
			{
				var p = new P(1, j);
				if ((p.Nexts().Sum(q => a[q.i][q.j]) >= 3 ? 1 : 0) != a[1][j])
				{
					valid[x] = false;
					break;
				}
			}
		}

		var dp = NewArray2<long>(21, 1 << 12);
		dp[0][0] = 1;
		for (int i = 1; i <= 20; i++)
		{
			for (int x = 0; x < 1 << 12; x++)
			{
				if (dp[i - 1][x] == 0) continue;
				for (int y = 0; y < 1 << 6; y++)
				{
					if (valid[(y << 12) | x] && Matches(i + 1, y))
						dp[i][(y << 6) | (x >> 6)] += dp[i - 1][x];
				}
			}
		}
		Console.WriteLine(dp[20][0]);

		bool Matches(int i, int x)
		{
			for (int xi = 0; xi < 6; xi++)
			{
				var c = s[i][xi];
				var expected = (x >> xi) & 1;
				if (c == 1 - expected) return false;
			}
			return true;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j), this };
	}
}
