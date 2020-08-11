using System;

class A
{
	static void Main() => Console.WriteLine(360 / Gcd(360, int.Parse(Console.ReadLine())));
	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
