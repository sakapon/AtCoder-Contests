using System;

class Q042
{
	const long M = 1000000007;
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = int.Parse(Console.ReadLine());

		if (k % 9 != 0) return 0;

		var dp = new MemoDP1<long>(k + 1, -1, (t, i) =>
		{
			var v = 0L;

			for (int j = Math.Max(0, i - 9); j < i; j++)
			{
				v += t[j];
			}
			return v % M;
		});
		dp[0] = 1;

		return dp[k];
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
