using System;
using System.Linq;

class K
{
	const long M = 1000000000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());
		var a = new int[k].Select(_ => { Console.ReadLine(); return Read(); }).ToArray();
		Console.ReadLine();
		var b = Read();

		var t = a.Select(ai => Tally(ai, 20)).ToArray();
		var inv = a.Select(ai =>
		{
			var sum = 0L;
			var bit = new BIT(20);
			foreach (var x in ai)
			{
				sum += bit.Sum(x + 1, 21);
				bit.Add(x, 1);
			}
			return sum;
		}).ToArray();

		var st = new BIT(20);
		var r = 0L;

		foreach (var bi in b)
		{
			for (int x = 1; x <= 20; x++)
			{
				r = (r + st.Sum(x + 1, 21) * t[bi - 1][x]) % M;
			}
			r = (r + inv[bi - 1]) % M;

			for (int x = 1; x <= 20; x++)
			{
				st.Add(x, t[bi - 1][x]);
			}
		}

		Console.WriteLine(r);
	}

	static int[] Tally(int[] a, int max)
	{
		var c = new int[max + 1];
		foreach (var x in a) ++c[x];
		return c;
	}
}

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
