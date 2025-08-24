using CoderLib8.DataTrees.SBTs;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var qs = Array.ConvertAll(new bool[m], _ => Read3());

		var monoid = new Monoid<(MInt p, MInt q)>((m2, m1) => (m1.p * m2.p, m1.q * m2.p + m2.q), (1, 0));
		var st = new PushSBT<(MInt p, MInt q)>(n, monoid);

		foreach (var (l, r, x) in qs)
		{
			var d = new MInt(1) / (r - l + 1);
			st[l - 1, r] = (d * (r - l), d * x);
		}

		var ms = st.ToArray();
		return string.Join(" ", Enumerable.Range(0, n).Select(i => a[i] * ms[i].p + ms[i].q));
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
