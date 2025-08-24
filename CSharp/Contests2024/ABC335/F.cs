class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		const int rt = 400;

		var r = 0L;
		var dp = new long[n];
		dp[0] = 1;

		// i: x の値
		// j: インデックス (mod x)
		var temp = new SeqArray2<long>(rt, rt);

		for (int i = 0; i < n; i++)
		{
			for (int x = 1; x < rt; x++)
			{
				dp[i] += temp[x, i % x];
			}
			dp[i] %= M;
			r += dp[i];

			if (a[i] < rt)
			{
				temp[a[i], i % a[i]] += dp[i];
				temp[a[i], i % a[i]] %= M;
			}
			else
			{
				for (int ni = i + a[i]; ni < n; ni += a[i])
				{
					dp[ni] += dp[i];
					dp[ni] %= M;
				}
			}
		}

		return r % M;
	}

	const long M = 998244353;
}

public class SeqArray2<T> : IEnumerable<ArraySegment<T>>
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
	public ArraySegment<T> this[int i] => new ArraySegment<T>(a, n2 * i, n2);
	public T[] ToArray(int i) => a[(n2 * i)..(n2 * (i + 1))];

	public void Fill(int i, T v) => Array.Fill(a, v, n2 * i, n2);
	public void Fill(T v) => Array.Fill(a, v);
	public void Clear() => Array.Clear(a, 0, a.Length);

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < n1; ++i) yield return this[i]; }
}
