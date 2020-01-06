using System;
using System.Linq;

class B2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(Enumerable.Range(1, 9).Any(i => n % i == 0 && n / i <= 9) ? "Yes" : "No");
	}
}
