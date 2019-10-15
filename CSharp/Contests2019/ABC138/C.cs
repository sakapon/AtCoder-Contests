using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		Console.WriteLine(Console.ReadLine().Split().Select(double.Parse).OrderBy(x => x).Aggregate((x, y) => (x + y) / 2));
	}
}
