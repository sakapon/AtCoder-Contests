using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(Array.IndexOf(a, 1) + 1);
	}
}
