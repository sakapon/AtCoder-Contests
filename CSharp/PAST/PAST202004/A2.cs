using System;

class A2
{
	static void Main()
	{
		var f = Array.ConvertAll(Console.ReadLine().Split(), s => s[1] == 'F' ? Subtract(s[0], '1') : Subtract('0', s[1]));
		Console.WriteLine(Abs(Subtract(f[0], f[1])));
	}

	static int Subtract(int x, int y)
	{
		var xor = x ^ y;
		return (y = (xor & y) << 1) == 0 ? xor : Subtract(xor, y);
	}

	static int Abs(int x) => (x & 1 << 31) == 0 ? x : ~Subtract(x, 1);
}
