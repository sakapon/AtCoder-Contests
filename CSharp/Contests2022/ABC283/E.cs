using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		const int max = 1 << 30;
		var iso = Array.ConvertAll(a, IsoForRow);

		// 1: 現在の行を反転
		// 2: 上の行を反転
		var dp = new int[4];
		var dt = new int[4];
		var states = new bool[4][];
		var statest = new bool[4][];

		Array.Fill(dp, max);
		Array.Fill(dt, max);
		dp[0] = 0;
		dp[1] = 1;
		states[0] = iso[0];
		states[1] = iso[0];

		for (int i = 1; i < h; i++)
		{
			for (int k = 0; k < 4; k++)
			{
				if (states[k] == null) continue;

				{
					var s = Next(i, k, false);
					if (s != null)
					{
						var nk = (k << 1) & 3;
						ChFirstMin(ref dt[nk], dp[k]);
						statest[nk] = s;
					}
				}
				{
					var s = Next(i, k, true);
					if (s != null)
					{
						var nk = (k << 1) & 3;
						nk |= 1;
						ChFirstMin(ref dt[nk], dp[k] + 1);
						statest[nk] = s;
					}
				}
			}

			(dp, dt) = (dt, dp);
			(states, statest) = (statest, states);
			Array.Fill(dt, max);
			Array.Fill(statest, null);
		}

		var r = Enumerable.Range(0, 4).Min(k => states[k] != null && states[k].All(b => !b) ? dp[k] : max);
		return r != max ? r : -1;

		// 行単独で孤立しているかどうか
		bool[] IsoForRow(int[] r)
		{
			var b = new bool[w];
			b[0] = r[0] != r[1];
			b[^1] = r[^2] != r[^1];
			for (int j = 1; j < w - 1; j++)
			{
				b[j] = r[j - 1] != r[j] && r[j] != r[j + 1];
			}
			return b;
		}

		// 上の行が成り立つかをチェックし、現在の行の孤立状態を返します。
		bool[] Next(int i, int k, bool reverse)
		{
			reverse ^= (k & 1) != 0;
			for (int j = 0; j < w; j++)
			{
				if (!states[k][j]) continue;
				if ((a[i - 1][j] != a[i][j]) ^ reverse) return null;
			}

			var b = new bool[w];
			for (int j = 0; j < w; j++)
			{
				if (!iso[i][j]) continue;
				if ((a[i - 1][j] != a[i][j]) ^ reverse) b[j] = true;
			}
			return b;
		}
	}

	public static void ChFirstMin<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) > 0) o1 = o2; }
}
