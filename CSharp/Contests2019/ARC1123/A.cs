using System;
using System.Linq;

class A
{
	static void Main()
	{
		var s = Console.ReadLine().Split().Select(int.Parse).Sum(x => Math.Max(0, 4 - x));
		Console.WriteLine(100000 * (s == 6 ? 10 : s));
	}
}
