using System;

class T
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];

		var tw = new MTwelvefold(k);

		//// A
		//Console.WriteLine(tw.Power(n));
		//// C
		//Console.WriteLine(tw.Surjection(n));
		//// G
		//Console.WriteLine(tw.Bell(n));
		//// I
		//Console.WriteLine(tw.Stirling(n));
		//// J
		//Console.WriteLine(tw.Partition(n));
		//// L
		//Console.WriteLine(tw.PartitionPositive(n));
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

	// n balls, k boxes
	// xPr, xCr を O(1) で求めるため、階乗を O(k) で求めておきます。
	int k;
	long[] f, f_;
	public MTwelvefold(int boxes)
	{
		k = boxes;
		f = MFactorials(k);
		f_ = Array.ConvertAll(f, MInv);
	}

	public long MFactorial(int n) => f[n];
	public long MInvFactorial(int n) => f_[n];
	public long MNpr(int n, int r) => f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => f[n] * f_[n - r] % M * f_[r] % M;

	#region Calculation

	public long Surjection(int balls)
	{
		// 包除原理により、0個になる場合を除きます。
		var r = 0L;
		for (int i = 0; i < k; ++i)
			r = MInt(r + (i % 2 == 0 ? 1 : -1) * MNcr(k, i) * MPow(k - i, balls));
		return r;
	}

	public long Stirling(int balls) => f_[k] * Surjection(balls) % M;

	public long Bell(int balls)
	{
		var t = new long[k + 1];
		t[0] = 1;
		for (int i = 1; i <= k; ++i)
			t[i] = MInt(t[i - 1] + (i % 2 == 0 ? 1 : -1) * f_[i]);

		var r = 0L;
		for (int i = 1; i <= k; ++i)
			r = MInt(r + MPow(i, balls) * f_[i] % M * t[k - i]);
		return r;
	}

	public static long[,] PartitionDP(int balls, int boxes)
	{
		var n = balls;
		var k = boxes;
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

	public static long Partition(int balls, int boxes) => PartitionDP(balls, boxes)[balls, boxes];
	public static long PartitionPositive(int balls, int boxes) => balls < boxes ? 0 : Partition(balls - boxes, boxes);

	#endregion

	#region Ways

	// A: 球: 区別する、箱: 区別する、箱に対する個数は自由
	// 各球に対し、独立に箱を選びます。
	public static long Way01(int balls, int boxes) => MPow(boxes, balls);

	// B: 球: 区別する、箱: 区別する、箱に1個以下
	// 単射の数です。
	// 各球の入る箱を順番に選びます。
	public long Way02(int balls) => MNpr(k, balls);

	// C: 球: 区別する、箱: 区別する、箱に1個以上
	// 全射の数です。
	public long Way03(int balls) => Surjection(balls);

	// D: 球: 区別しない、箱: 区別する、箱に対する個数は自由
	// 球と区切りを並べ替えます。
	public static long Way04(int balls, int boxes) => new MTwelvefold(balls + boxes).MNcr(balls + boxes - 1, balls);

	// E: 球: 区別しない、箱: 区別する、箱に1個以下
	// 球の入る箱を選ぶ組合せを求めます。
	public long Way05(int balls) => MNcr(k, balls);

	// F: 球: 区別しない、箱: 区別する、箱に1個以上
	// 球を一列に並べ、区切る位置を選ぶ組合せを求めます。
	public static long Way06(int balls, int boxes) => new MTwelvefold(balls).MNcr(balls - 1, boxes - 1);

	// G: 球: 区別する、箱: 区別しない、箱に対する個数は自由
	public long Way07(int balls) => Bell(balls);

	// H: 球: 区別する、箱: 区別しない、箱に1個以下
	public static long Way08(int balls, int boxes) => balls > boxes ? 0 : 1;

	// I: 球: 区別する、箱: 区別しない、箱に1個以上
	public long Way09(int balls) => Stirling(balls);

	// J: 球: 区別しない、箱: 区別しない、箱に対する個数は自由
	// 非負整数への分割数です。
	public static long Way10(int balls, int boxes) => Partition(balls, boxes);

	// K: 球: 区別しない、箱: 区別しない、箱に1個以下
	public static long Way11(int balls, int boxes) => balls > boxes ? 0 : 1;

	// L: 球: 区別しない、箱: 区別しない、箱に1個以上
	// 正整数への分割数です。
	public static long Way12(int balls, int boxes) => PartitionPositive(balls, boxes);

	#endregion
}
