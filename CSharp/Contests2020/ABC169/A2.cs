using System;
using System.Linq;

class A2
{
	static void Main() => Console.WriteLine(Console.ReadLine().Split().Select(int.Parse).Aggregate((x, y) => x * y));
}
