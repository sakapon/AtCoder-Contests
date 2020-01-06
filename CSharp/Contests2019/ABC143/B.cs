using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(Enumerable.Range(0, n).SelectMany(i => Enumerable.Range(i + 1, n - i - 1).Select(j => d[i] * d[j])).Sum());
	}
}
