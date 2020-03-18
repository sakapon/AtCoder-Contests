using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		Console.WriteLine(a.Max(x => x.Sum()));
	}
}
