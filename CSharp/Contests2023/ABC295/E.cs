class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var a = Read();

		k--;
		a = a.Where(x => x != 0).OrderBy(x => x).ToArray();
		var c0 = n - a.Length;

		var cs = new int[m + 1];
		foreach (var x in a) cs[x]++;

		var s = new int[m + 1];
		for (int i = 1; i <= m; i++) s[i] = s[i - 1] + cs[i];

		var m_ = (MInt)1 / m;
		var mc = new MCombination(2000);

		return Enumerable.Range(1, m).Select(v => GetProb(v)).Aggregate((x, y) => x + y).V;

		// k 番目が v 以上となる確率
		MInt GetProb(int v)
		{
			if (k < a.Length && a[k] < v) return 0;

			// c0 個のうち、c 個は v 未満、c0 - c 個は v 以上
			MInt r = 0;
			for (int c = 0; c <= c0; c++)
			{
				if (k < s[v - 1] + c) continue;
				r += mc.MNcr(c0, c) * ((v - 1) * m_).Pow(c) * ((m - v + 1) * m_).Pow(c0 - c);
			}
			return r;
		}
	}
}

struct MInt
{
	//const long M = 1000000007;
	const long M = 998244353;
	public long V;
	public MInt(long v) { V = (v %= M) < 0 ? v + M : v; }
	public override string ToString() => $"{V}";
	public static implicit operator MInt(long v) => new MInt(v);

	public static MInt operator -(MInt x) => -x.V;
	public static MInt operator +(MInt x, MInt y) => x.V + y.V;
	public static MInt operator -(MInt x, MInt y) => x.V - y.V;
	public static MInt operator *(MInt x, MInt y) => x.V * y.V;
	public static MInt operator /(MInt x, MInt y) => x.V * y.Inv().V;

	public static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	public MInt Pow(long i) => MPow(V, i);
	public MInt Inv() => MPow(V, M - 2);
}

public class MCombination
{
	//const long M = 1000000007;
	const long M = 998244353;
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

	// nPr, nCr を O(1) で求めるため、階乗を O(n) で求めておきます。
	long[] f, f_;
	public MCombination(int nMax)
	{
		f = MFactorials(nMax);
		f_ = Array.ConvertAll(f, MInv);
	}

	public long MFactorial(int n) => f[n];
	public long MInvFactorial(int n) => f_[n];
	public long MNpr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M * f_[r] % M;

	// nMax >= 2n としておく必要があります。
	public long MCatalan(int n) => f[2 * n] * f_[n] % M * f_[n + 1] % M;
}
