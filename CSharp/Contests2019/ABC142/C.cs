using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		Console.WriteLine(string.Join(" ", Console.ReadLine().Split().Select((s, i) => new { x = int.Parse(s), i }).OrderBy(_ => _.x).Select(_ => _.i + 1)));
	}
}
