using System;
using System.Linq;

class C
{
	static void Main()
	{
		var s = Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(string.Join(" ", a));
	}
}
