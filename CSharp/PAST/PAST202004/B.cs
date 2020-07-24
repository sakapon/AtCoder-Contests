using System;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Console.ReadLine().GroupBy(x => x).OrderBy(g => -g.Count()).First().Key);
}
