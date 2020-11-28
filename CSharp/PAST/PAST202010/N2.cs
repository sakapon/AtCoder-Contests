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

		var s2 = NewArray2(s.Count, 1 << 6, true);
		for (int i = 0; i < s.Count; i++)
			for (int x = 0; x < 1 << 6; x++)
				for (int xi = 0; xi < 6; xi++)
				{
					var expected = (x >> xi) & 1;
					if (s[i][xi] == 1 - expected)
					{
						s2[i][x] = false;
						break;
					}
				}

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
		for (int i = 0; i < 20; i++)
			for (int x = 0; x < 1 << 12; x++)
			{
				if (dp[i][x] == 0) continue;
				for (int y = 0; y < 1 << 6; y++)
				{
					var xy = (y << 12) | x;
					if (valid[xy] && s2[i + 2][y])
						dp[i + 1][xy >> 6] += dp[i][x];
				}
			}
		Console.WriteLine(dp[20][0]);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public P[] Nexts() => new[] { new P(i, j - 1), new P(i, j + 1), new P(i - 1, j), new P(i + 1, j), this };
	}
}
