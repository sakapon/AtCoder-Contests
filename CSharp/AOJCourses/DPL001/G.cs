using System;
using System.Linq;

class G
{
	const int min = -1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new int[n], _ => Read());

		var dp = NewArray1(w + 1, min);
		dp[0] = 0;

		foreach (var p in ps)
		{
			for (int i = w - 1; i >= 0; i--)
			{
				if (dp[i] == min) continue;

				for (int j = 1; j <= p[2]; j++)
				{
					var ni = i + j * p[1];
					var nv = dp[i] + j * p[0];
					if (ni > w || dp[ni] >= nv) break;
					dp[ni] = nv;
				}
			}
		}
		Console.WriteLine(dp.Max());
	}

	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}
}
