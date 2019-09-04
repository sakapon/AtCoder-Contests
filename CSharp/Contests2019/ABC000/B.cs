using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		Console.WriteLine(string.Join(" ", a));
	}
}
