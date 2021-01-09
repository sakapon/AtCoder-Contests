using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		Console.WriteLine(Console.ReadLine().Split().Select(int.Parse).Aggregate(Gcd));
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
