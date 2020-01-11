using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(a.Max());
	}
}
