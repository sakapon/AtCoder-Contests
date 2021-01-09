using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(a.Aggregate(Lcm));
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
	static int Lcm(int a, int b) => a / Gcd(a, b) * b;
}
