using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => int.Parse(Console.ReadLine())).ToArray();
		Console.WriteLine(string.Join("\n", qs.Select(q => Array.BinarySearch(a, q) >= 0 ? 1 : 0)));
	}
}
