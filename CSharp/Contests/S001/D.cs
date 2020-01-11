using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(string.Join(" ", a.OrderBy(x => x)));
	}
}
