using System;
using System.Linq;

class B2
{
	static void Main()
	{
		long x = long.Parse(Console.ReadLine()), c = 100;
		Console.WriteLine(Enumerable.Range(1, 3760).First(i => (c += c / 100) >= x));
	}
}
