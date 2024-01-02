class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var half = new MInt(1) / 2;

		var dp = new SeqArray2<MInt>(n + 1, n + 1);
		dp[1, 1] = 1;

		for (int i = 2; i <= n; i++)
		{
			MInt p2 = 1;
			for (int j = 1; j < i; j++)
			{
				dp[i, 1] += dp[i - 1, j] * p2;
				p2 *= 2;
			}
			p2 *= 2;
			dp[i, 1] /= p2 - 1;

			for (int j = 2; j <= i; j++)
			{
				dp[i, j] = (dp[i, j - 1] + dp[i - 1, j - 1]) * half;
			}
		}
		return string.Join(" ", dp[n][1..]);
	}
}

struct MInt
{
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

public class SeqArray2<T>
{
	public readonly int n1, n2;
	public readonly T[] a;
	public SeqArray2(int _n1, int _n2, T[] _a = null) => (n1, n2, a) = (_n1, _n2, _a ?? new T[_n1 * _n2]);
	public SeqArray2(int _n1, int _n2, T iv) : this(_n1, _n2, default(T[])) => Array.Fill(a, iv);

	public T this[int i, int j]
	{
		get => a[n2 * i + j];
		set => a[n2 * i + j] = value;
	}
	public T[] this[int i] => a[(n2 * i)..(n2 * (i + 1))];
	public ArraySegment<T> GetSegment(int i) => new ArraySegment<T>(a, n2 * i, n2);

	public void Fill(T v) => Array.Fill(a, v);
	public void Fill(int i, T v) => Array.Fill(a, v, n2 * i, n2);
}
