using System;
using System.Linq;

class D2
{
	static void Main()
	{
		var h = long.Parse(Console.ReadLine());
		Console.WriteLine(new int[41].Select((_, i) => (1L << i) - 1).First(x => x >= h));
	}
}
