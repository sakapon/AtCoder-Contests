using System;
using System.Linq;

class B
{
	static void Main() { foreach (var x in Enumerable.Range(1, int.Parse(Console.ReadLine())).Select(i => ($"{i} " + Console.ReadLine()).Split()).OrderBy(x => x[1]).ThenByDescending(x => int.Parse(x[2]))) Console.WriteLine(x[0]); }
}
