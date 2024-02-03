using System;

class A3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		f = new long[n + 1];
		Console.WriteLine(Rec(n));
	}

	// TLE
	static long[] f;
	static long Rec(int n)
	{
		if (n <= 1) return 1;
		return f[n] = Rec(n - 1) + Rec(n - 2);
	}

	// TLE
	static long Rec0(int n)
	{
		if (n <= 1) return 1;
		return Rec0(n - 1) + Rec0(n - 2);
	}
}
