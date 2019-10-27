using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Console.WriteLine(Enumerable.Range(1, (int)Math.Sqrt(n)).Where(x => n % x == 0).Min(x => x + n / x - 2));
	}
}
