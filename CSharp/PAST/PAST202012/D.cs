using System;
using System.Linq;
using System.Numerics;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		Console.WriteLine(string.Join("\n", s.OrderBy(BigInteger.Parse).ThenBy(x => -x.Length)));
	}
}
