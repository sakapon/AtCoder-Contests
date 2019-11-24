using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var s = Console.ReadLine();
		var n = int.Parse(Console.ReadLine());
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		Console.WriteLine(string.Join(" ", h));
	}
}
