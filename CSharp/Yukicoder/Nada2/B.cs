using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(string.Join(" ", Enumerable.Range(0, n).Select(i => 1L << i)));
	}
}
