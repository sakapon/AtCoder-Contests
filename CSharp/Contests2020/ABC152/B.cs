using System;
using System.Linq;

class B
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(new string(h.Min().ToString()[0], h.Max()));
	}
}
