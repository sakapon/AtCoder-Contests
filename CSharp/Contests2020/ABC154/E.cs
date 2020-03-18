using System;

class E
{
	static void Main() => Console.WriteLine(Find(Console.ReadLine(), int.Parse(Console.ReadLine())));

	static int Find(string n, int k)
	{
		var r = (int)Ncr(n.Length - 1, k) * (int)Math.Pow(9, k);
		r += (n[0] - '0' - 1) * (int)Ncr(n.Length - 1, k - 1) * (int)Math.Pow(9, k - 1);
		if (k == 1)
		{
			r += 1;
		}
		else
		{
			n = n.Remove(0, 1).TrimStart('0');
			if (n != "") r += Find(n, k - 1);
		}
		return r;
	}

	static long Factorial(int n) { for (long x = 1, i = 1; ; x *= ++i) if (i >= n) return x; }
	static long Npr(int n, int r)
	{
		if (n < r) return 0;
		for (long x = 1, i = n - r; ; x *= ++i) if (i >= n) return x;
	}
	static long Ncr(int n, int r) => n < r ? 0 : n - r < r ? Ncr(n, n - r) : Npr(n, r) / Factorial(r);
}
