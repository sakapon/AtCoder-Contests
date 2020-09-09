using System;

class C
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());

		var r = 0;
		for (int a = 1; a <= k; a++)
			for (int b = 1; b <= k; b++)
				for (int c = 1; c <= k; c++)
					r += Gcd(Gcd(a, b), c);
		Console.WriteLine(r);
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
