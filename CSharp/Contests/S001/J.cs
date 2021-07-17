using System;
using System.Linq;

class J
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(InversionNumber(n, a));
	}

	public static long InversionNumber(int n, int[] a)
	{
		var r = 0L;
		var bit = new BIT(n);
		for (int i = 0; i < n; ++i)
		{
			r += i - bit.Sum(a[i]);
			bit.Add(a[i], 1);
		}
		return r;
	}
}

// 機能限定版
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

	public long this[int i] => Sum(i) - Sum(i - 1);

	public void Add(int i, long v)
	{
		for (; i <= n2; i += i & -i) a[i] += v;
	}

	public long Sum(int r_in)
	{
		var r = 0L;
		for (var i = r_in; i > 0; i -= i & -i) r += a[i];
		return r;
	}
}
