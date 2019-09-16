using System;
using System.Linq;

class B
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(new string('#', a[1] + 2));
		for (var i = 0; i < a[0]; i++) Console.WriteLine($"#{Console.ReadLine()}#");
		Console.WriteLine(new string('#', a[1] + 2));
	}
}
