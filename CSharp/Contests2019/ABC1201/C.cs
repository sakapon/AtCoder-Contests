using System;
using System.Linq;

class C
{
	static void Main()
	{
		var x = int.Parse(Console.ReadLine());
		Console.WriteLine(Enumerable.Range(1, 1000).Any(i => 100 * i <= x && x <= 105 * i) ? 1 : 0);
	}
}
