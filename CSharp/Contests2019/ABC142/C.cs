using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select((x, i) => new { x = int.Parse(x), i = i + 1 }).OrderBy(_ => _.x).ToArray();
		Console.WriteLine(string.Join(" ", a.Select(_ => _.i)));
	}
}
