using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = double.Parse(Console.ReadLine());
		Console.WriteLine(n % 2 == 0 ? 0.5 : (n + 1) / 2 / n);
	}
}
