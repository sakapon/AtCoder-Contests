using System;

class A3
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		Console.WriteLine(Rec(n));
	}

	// TLE
	static long Rec(int n)
	{
		if (n <= 1) return 1;
		return Rec(n - 1) + Rec(n - 2);
	}
}
