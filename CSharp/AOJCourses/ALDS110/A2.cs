using System;

class A2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		f = new long[n + 1];
		Array.Fill(f, -1);
		f[1] = f[0] = 1;
		Console.WriteLine(Rec(n));
	}

	// メモ化再帰
	static long[] f;
	static long Rec(int n)
	{
		if (f[n] != -1) return f[n];
		return f[n] = Rec(n - 1) + Rec(n - 2);
	}
}
