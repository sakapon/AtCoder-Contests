using System;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(Math.DivRem(n, 25, out n) + Math.DivRem(n, 10, out n) + Math.DivRem(n, 5, out n) + n);
	}
}
