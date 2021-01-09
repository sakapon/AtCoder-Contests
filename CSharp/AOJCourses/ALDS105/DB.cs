using System;
using System.Linq;

class DB
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).Select((x, i) => new { x, i }).OrderBy(_ => _.x).ToArray();

		var st = new BIT(n);
		var r = 0L;
		for (int i = 0; i < n; i++)
		{
			r += st.Sum(a[i].i + 1, n + 1);
			st.Add(a[i].i + 1, 1);
		}
		Console.WriteLine(r);
	}
}

// 外見上は 1-indexed, 1 <= i <= n
// 内部では 1-indexed, 1 <= i <= n2
class BIT
{
	// Power of 2
	int n2 = 1;
	long[] a;

	public BIT(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 + 1];
	}

	public long this[int i]
	{
		get { return Sum(i) - Sum(i - 1); }
		set { Add(i, value - this[i]); }
	}

	public void Add(int i, long v)
	{
		for (; i <= n2; i += i & -i) a[i] += v;
	}

	public long Sum(int l_in, int r_ex) => Sum(r_ex - 1) - Sum(l_in - 1);
	public long Sum(int r_in)
	{
		var r = 0L;
		for (var i = r_in; i > 0; i -= i & -i) r += a[i];
		return r;
	}
}
