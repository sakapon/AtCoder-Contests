using System;
using System.Linq;

class C2
{
	static void Main() => Console.WriteLine(Math.Floor(Console.ReadLine().Split().Select(decimal.Parse).Aggregate((x, y) => x * y)));
}
