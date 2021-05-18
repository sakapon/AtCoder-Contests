using System;

class BR
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		var dp = new MemoDP1<long>(n, -1, (t, i) => (t[i - 2] + t[i - 1]) % m);
		dp[0] = 0;
		dp[1] = 1;
		return dp[n - 1];
	}
}

class MemoDP1<T>
{
	static readonly Func<T, T, bool> TEquals = System.Collections.Generic.EqualityComparer<T>.Default.Equals;
	public T[] Raw { get; }
	T iv;
	Func<MemoDP1<T>, int, T> rec;

	public MemoDP1(int n, T iv, Func<MemoDP1<T>, int, T> rec)
	{
		Raw = Array.ConvertAll(new bool[n], _ => iv);
		this.iv = iv;
		this.rec = rec;
	}

	public T this[int i]
	{
		get => TEquals(Raw[i], iv) ? Raw[i] = rec(this, i) : Raw[i];
		set => Raw[i] = value;
	}
}
