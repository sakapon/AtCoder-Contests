using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		Console.WriteLine(Enumerable.Range(1, n - 1).Sum(i => (n - 1) / i));
	}
}
