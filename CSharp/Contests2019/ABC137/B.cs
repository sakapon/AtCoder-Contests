using System;
using System.Linq;

class B
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(string.Join(" ", Enumerable.Range(a[1] - a[0] + 1, 2 * a[0] - 1)));
	}
}
