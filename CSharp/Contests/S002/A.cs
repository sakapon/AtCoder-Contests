using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		Console.WriteLine(string.Join("\n", a.Select(x => (long)x[0] * x[1])));
	}
}
