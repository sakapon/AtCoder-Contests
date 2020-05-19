using System;

class A2
{
	static void Main()
	{
		int n;
		Console.WriteLine(int.TryParse(Console.ReadLine(), out n) ? $"{Add(n, n)}" : "error");
	}

	static int Add(int x, int y)
	{
		var xor = x ^ y;
		return (y = (x & y) << 1) == 0 ? xor : Add(xor, y);
	}
}
