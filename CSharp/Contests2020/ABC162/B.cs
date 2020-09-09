using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Enumerable.Range(1, int.Parse(Console.ReadLine())).Where(x => x % 3 > 0 && x % 5 > 0).Sum(x => (long)x));
}
