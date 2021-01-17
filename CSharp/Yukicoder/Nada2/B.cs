using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Pows2L();
		Console.WriteLine(string.Join(" ", ps.Take(n)));
	}

	static long[] Pows2L() => Enumerable.Range(0, 63).Select(i => 1L << i).ToArray();
}
