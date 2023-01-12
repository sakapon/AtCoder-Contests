using System;
using System.Collections.Generic;
using System.Linq;

class P064
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int nmax = 10000;
		var sqset = Enumerable.Range(1, 100).Select(i => (long)i * i).ToHashSet();

		return Enumerable.Range(1, nmax).Count(n => !sqset.Contains(n) && GetPeriod(n) % 2 == 1);
	}

	static int GetPeriod(int n)
	{
		var set = new HashSet<Irrational>();
		var x = new Irrational(n, 0, 1);

		for (int k = 0; ; k++)
		{
			x = Next(x).Item2;
			if (set.Contains(x)) return k;
			set.Add(x);
		}
	}

	// (整数部分, 小数部分の逆数)
	static (long, Irrational) Next(Irrational x)
	{
		long i = (long)Math.Floor(x.Value);
		return (i, new Irrational(x.a, x.b - i * x.c, x.c).Inverse);
	}
}

// (√a + b) / c
[System.Diagnostics.DebuggerDisplay(@"{Value}")]
public struct Irrational
{
	public long a, b, c;
	public double Value => (Math.Sqrt(a) + b) / c;

	public Irrational(long a, long b, long c)
	{
		this.a = a;
		this.b = b;
		this.c = c;
	}

	// c * (√a - b) / (a - b * b)
	public Irrational Inverse
	{
		get
		{
			// 必ず c で割り切れる？
			return new Irrational(a, -b, (a - b * b) / c);
		}
	}
}
