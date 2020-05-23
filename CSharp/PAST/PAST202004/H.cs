using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	class VC
	{
		public V v;
		public int count = 1 << 30;
	}

	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var a = new int[n].Select(_ => Console.ReadLine()).ToArray();

		var dp = new int[11].Select(_ => new List<VC>()).ToArray();
		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
			{
				var c = a[i][j];
				dp[c == 'S' ? 0 : c == 'G' ? 10 : c - '0'].Add(new VC { v = new V(i, j) });
			}

		dp[0][0].count = 0;
		for (int i = 1; i <= 10; i++)
			foreach (var vc in dp[i])
				foreach (var vc_ in dp[i - 1])
					vc.count = Math.Min(vc.count, vc_.count + (vc.v - vc_.v).Norm);
		var r = dp[10][0].count;
		Console.WriteLine(r < 1 << 30 ? r : -1);
	}
}

struct V
{
	public int X, Y;
	public V(int x, int y) { X = x; Y = y; }

	public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);
	public int Norm => Math.Abs(X) + Math.Abs(Y);
}
