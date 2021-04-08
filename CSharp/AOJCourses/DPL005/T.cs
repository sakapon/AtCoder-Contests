using System;

class T
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];
		Console.WriteLine(MTwelvefold.Way01(n, k));
	}
}

public class MTwelvefold
{
	//const long M = 998244353;
	const long M = 1000000007;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);

	static long[] MFactorials(int n)
	{
		var f = new long[n + 1];
		f[0] = 1;
		for (int i = 1; i <= n; ++i) f[i] = f[i - 1] * i % M;
		return f;
	}

	// xPr, xCr を O(1) で求めるため、階乗を O(k) で求めておきます。
	long[] f, f_;
	public MTwelvefold(int kMax)
	{
		f = MFactorials(kMax);
		f_ = Array.ConvertAll(f, MInv);
	}

	public long MFactorial(int n) => f[n];
	public long MInvFactorial(int n) => f_[n];
	public long MNpr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M * f_[r] % M;

	#region Calculation

	public long Surjection(int n, int k)
	{
		// 包除原理により、0個になる場合を除きます。
		var r = 0L;
		for (int i = 0; i < k; ++i)
			r = MInt(r + (i % 2 == 0 ? 1 : -1) * MNcr(k, i) * MPow(k - i, n));
		return r;
	}

	public long Stirling(int n, int k) => f_[k] * Surjection(n, k) % M;

	public long Bell(int n, int k)
	{
		var t = new long[k + 1];
		t[0] = 1;
		for (int i = 1; i <= k; ++i)
			t[i] = MInt(t[i - 1] + (i % 2 == 0 ? 1 : -1) * f_[i]);

		var r = 0L;
		for (int i = 1; i <= k; ++i)
			r = MInt(r + MPow(i, n) * f_[i] % M * t[k - i]);
		return r;
	}

	public static long[,] PartitionDP(int n, int k)
	{
		var dp = new long[n + 1, k + 1];
		dp[0, 0] = 1;

		for (int i = 0; i <= n; ++i)
			for (int j = 1; j <= k; ++j)
			{
				dp[i, j] = dp[i, j - 1];
				if (i >= j) dp[i, j] = (dp[i, j] + dp[i - j, j]) % M;
			}
		return dp;
	}

	// k 個の非負整数への分割数です。
	public static long Partition(int n, int k) => PartitionDP(n, k)[n, k];
	// k 個の正整数への分割数です。
	public static long PartitionPositive(int n, int k) => n < k ? 0 : Partition(n - k, k);

	#endregion

	#region Ways
	// n balls, k boxes
	// 箱に1個以下のとき、n <= k
	// 箱に1個以上のとき、n >= k

	// A: 球: 区別する、箱: 区別する、箱に対する個数は自由
	// 各球に対し、独立に箱を選びます。
	public static long Way01(int balls, int boxes) => MPow(boxes, balls);

	// B: 球: 区別する、箱: 区別する、箱に1個以下
	// 単射の数です。
	// 各球の入る箱を順番に選びます。
	public static long Way02(int balls, int boxes) => new MTwelvefold(boxes).MNpr(boxes, balls);

	// C: 球: 区別する、箱: 区別する、箱に1個以上
	// 全射の数です。
	public static long Way03(int balls, int boxes) => new MTwelvefold(boxes).Surjection(balls, boxes);

	// D: 球: 区別しない、箱: 区別する、箱に対する個数は自由
	// 球と区切りを並べ替えます。
	public static long Way04(int balls, int boxes) => new MTwelvefold(balls + boxes).MNcr(balls + boxes - 1, balls);

	// E: 球: 区別しない、箱: 区別する、箱に1個以下
	// 球の入る箱を選ぶ組合せを求めます。
	public static long Way05(int balls, int boxes) => new MTwelvefold(boxes).MNcr(boxes, balls);

	// F: 球: 区別しない、箱: 区別する、箱に1個以上
	// 球を一列に並べ、区切る位置を選ぶ組合せを求めます。
	public static long Way06(int balls, int boxes) => new MTwelvefold(balls).MNcr(balls - 1, boxes - 1);

	// G: 球: 区別する、箱: 区別しない、箱に対する個数は自由
	public static long Way07(int balls, int boxes) => new MTwelvefold(boxes).Bell(balls, boxes);

	// H: 球: 区別する、箱: 区別しない、箱に1個以下
	public static long Way08(int balls, int boxes) => balls > boxes ? 0 : 1;

	// I: 球: 区別する、箱: 区別しない、箱に1個以上
	public static long Way09(int balls, int boxes) => new MTwelvefold(boxes).Stirling(balls, boxes);

	// J: 球: 区別しない、箱: 区別しない、箱に対する個数は自由
	public static long Way10(int balls, int boxes) => Partition(balls, boxes);

	// K: 球: 区別しない、箱: 区別しない、箱に1個以下
	public static long Way11(int balls, int boxes) => balls > boxes ? 0 : 1;

	// L: 球: 区別しない、箱: 区別しない、箱に1個以上
	public static long Way12(int balls, int boxes) => PartitionPositive(balls, boxes);

	#endregion
}
