using System;
using System.Linq;

class A
{
	static void Main()
	{
		var r9 = Enumerable.Range(1, 9);
		var f = r9.Select(i => $"B{i}").Reverse().Concat(r9.Select(i => $"{i}F")).ToArray();

		var h = Console.ReadLine().Split();
		Console.WriteLine(Math.Abs(Array.IndexOf(f, h[0]) - Array.IndexOf(f, h[1])));
	}
}
