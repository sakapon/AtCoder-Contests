using System;

class B
{
	struct P { public int X, Y; }

	static void Main()
	{
		Func<int[]> read = () => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var h = read();
		var a = read();
		var b = read();
		int A = h[0], B = h[1];

		var dp = new P[A + 1, B + 1];
		for (int k = A + B - 1; k >= 0; k--)
			for (int i = Math.Min(A, k), j; i >= 0 && (j = k - i) <= B; i--)
			{
				var pa = i < A ? new P { X = a[i] + dp[i + 1, j].Y, Y = dp[i + 1, j].X } : new P { X = -1 };
				var pb = j < B ? new P { X = b[j] + dp[i, j + 1].Y, Y = dp[i, j + 1].X } : new P { X = -1 };
				dp[i, j] = pa.X >= pb.X ? pa : pb;
			}
		Console.WriteLine(dp[0, 0].X);
	}
}
