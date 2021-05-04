using System;
using System.Collections.Generic;
using System.Linq;

class Q023
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var states = GetStates(w + 2);
		var n = states.Length;
		var statesMap = ToInverseMap(states, 1 << (w + 2));

		var f_last = 1 << (w + 1);
		// 厳密には、w=1 のとき、3 | f_last
		var f_king = 7 | f_last;

		// 左側 (j=0) に壁が追加されているものとする
		// マス (i, j) 以前の w+2 マスの状態 k における方法
		var dp = new long[n];
		dp[0] = 1;

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j <= w; j++)
			{
				dp = NextDP(dp, j < w && c[i][j] == '.');
			}
		}
		return dp.Sum() % M;

		long[] NextDP(long[] dp, bool isEmpty)
		{
			var t = new long[n];
			for (int k = 0; k < n; k++)
			{
				var ns = states[k] >> 1;
				t[statesMap[ns]] += dp[k];

				if (isEmpty && (states[k] & f_king) == 0)
					t[statesMap[ns | f_last]] += dp[k];
			}

			for (int k = 0; k < n; k++)
				t[k] %= M;
			return t;
		}
	}

	static int[] GetStates(int length)
	{
		var r = new List<int>();
		for (int x = 0; x < 1 << length; x++)
			if (IsValidState(x, length)) r.Add(x);
		return r.ToArray();
	}

	static bool IsValidState(int x, int length)
	{
		for (int i = 1; i < length; ++i)
			if ((x & (1 << i)) != 0 && (x & (1 << (i - 1))) != 0) return false;
		return true;
	}

	static int[] ToInverseMap(int[] a, int max)
	{
		var d = Array.ConvertAll(new bool[max + 1], _ => -1);
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}
}
