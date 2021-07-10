using System;
using System.Collections;
using System.Linq;

class Q088D
{
	const int max = 8888;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var a = Read().Prepend(0).ToArray();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var map = NewArray2<bool>(n + 1, n + 1);
		foreach (var (x, y) in es)
			map[x][y] = true;

		var dp = new BitArray[max + 1];
		dp[0] = new BitArray(n + 1);

		for (int i = 1; i <= n; i++)
		{
			for (int x = max - 1; x >= 0; x--)
			{
				if (dp[x] == null) continue;
				if (!CheckNext(dp[x], i)) continue;

				var nx = x + a[i];
				var b = new BitArray(dp[x]);
				b[i] = true;

				if (dp[nx] == null)
				{
					dp[nx] = b;
				}
				else
				{
					WriteBits(b);
					WriteBits(dp[nx]);
					goto End;
				}
			}
		}

	End:

		bool CheckNext(BitArray b, int ni)
		{
			for (int i = 1; i <= n; i++)
				if (b[i] && map[i][ni])
					return false;
			return true;
		}

		void WriteBits(BitArray b)
		{
			var r = Enumerable.Range(1, n).Where(i => b[i]).ToArray();
			Console.WriteLine(r.Length);
			Console.WriteLine(string.Join(" ", r));
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
