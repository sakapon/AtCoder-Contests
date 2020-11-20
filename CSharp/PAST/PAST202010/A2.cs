using System;
using System.Linq;

class A2
{
	static void Main()
	{
		var v = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine("ABC".OrderBy(c => v[c - 'A']).ElementAt(1));
	}
}
