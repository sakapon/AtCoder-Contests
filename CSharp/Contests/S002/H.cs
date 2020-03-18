using System;
using System.Linq;

class H
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		Console.WriteLine(string.Join("\n", a.Select(x => x.Max() - x.Min()).Select(x => x == 0 ? -1 : x)));
	}
}
