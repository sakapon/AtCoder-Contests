using System;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long, long, long) Read5L() { var a = ReadL(); return (a[0], a[1], a[2], a[3], a[4]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (X, Y, A, B, C) = Read5L();

		if (Check(X, Y, A, B, C)) return true;
		if (Check(X, Y, B, C, A)) return true;
		if (Check(X, Y, C, A, B)) return true;

		if (Check(Y, X, A, B, C)) return true;
		if (Check(Y, X, B, C, A)) return true;
		if (Check(Y, X, C, A, B)) return true;

		return false;
	}

	static bool Check(long X, long Y, long A, long B, long C)
	{
		var y = (C + X - 1) / X;
		if (y >= Y) return false;

		Y -= y;
		if (Check(X, Y, A, B)) return true;
		if (Check(Y, X, A, B)) return true;

		return false;
	}

	static bool Check(long X, long Y, long A, long B)
	{
		var y = (B + X - 1) / X;
		if (y >= Y) return false;

		Y -= y;
		return X * Y >= A;
	}
}
